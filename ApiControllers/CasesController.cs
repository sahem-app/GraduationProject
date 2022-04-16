﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GraduationProject.Data;
using GraduationProject.DTOs.Case;
using GraduationProject.Models;
using GraduationProject.Models.Reviews;
using GraduationProject.Utilities.CustomApiResponses;
using GraduationProject.Utilities.StaticStrings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.ApiControllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CasesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CasesController(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[Authorize]
		[HttpGet("[action]")]
		public async Task<IActionResult> Add()
		{
			var properties = new CaseProperties
			{
				Genders = await _context.Genders.AsNoTracking().ToArrayAsync(),
				SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync(),
				Relationships = await _context.Relationships.AsNoTracking().ToArrayAsync(),
				Categories = await _context.Categories.AsNoTracking().ToArrayAsync(),
				Periods = await _context.Periods.AsNoTracking().ToArrayAsync(),
				Priorities = await _context.Priorities.AsNoTracking().ToArrayAsync()
			};

			return new Success(properties);
		}

		//[HttpGet]
		//[Authorize]
		//public async Task<IActionResult> GetCases()
		//{
		//	var cases = await _context.Cases
		//		.Where(c => c.Status.Name == Status.Accepted)
		//		.Select(c => new CaseElementDto
		//		{
		//			Id = c.Id,
		//			Name = c.Name,
		//			Title = c.Title,
		//			Priority = c.Priority.Name,
		//			Age = ((short)(DateTime.Now - c.DateRequested).TotalDays),
		//			FundRaised = 4000,
		//			ImageUrl = string.Concat(Request.Scheme, "://", Request.Host, Request.PathBase.ToString().ToLower(), "/api/cases/image")
		//		}).ToArrayAsync();

		//	return new Success(cases);
		//}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Add([FromForm] NewCaseDto dto)
		{
			var result = await IsCasesAddedAsync(dto);
			if (result != null)
				return result;

			var pendingStatusId = _context.Status
				.Where(s => s.Name == Status.Pending)
				.Select(s => s.Id)
				.FirstAsync();

			var newCase = _mapper.Map<Case>(dto);
			var settingImageTask = newCase.SetNationalIdImageAsync(dto.NationalIdImage);

			if (dto.OptionalImages != null)
				newCase.AddOptionalImages(dto.OptionalImages);

			newCase.MediatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			newCase.StatusId = await pendingStatusId;
			await settingImageTask;
			await _context.AddAsync(newCase);
			await _context.SaveChangesAsync();
			return new Success();
		}

		[Authorize]
		[HttpPost("[action]")]
		public async Task<IActionResult> Reviews([FromForm] ReviewDto dto)
		{
			if (!await _context.Cases.AnyAsync(c => c.Id == dto.CaseId))
				return new BadRequest("Case was not found");

			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (await _context.CaseReviews.AnyAsync(c => c.CaseId == dto.CaseId && c.MediatorId == userId))
				return new BadRequest("Case has been reviewd already");

			var review = _mapper.Map(dto, new CaseReview(userId));
			await _context.CaseReviews.AddAsync(review);
			await _context.SaveChangesAsync();
			return new Success();
		}

		private async Task<BadRequest> IsCasesAddedAsync(NewCaseDto dto)
		{
			var caseDb = await _context.Cases
				.Select(m => new Case
				{
					NationalId = m.NationalId,
					PhoneNumber = m.PhoneNumber
				}).FirstOrDefaultAsync(m => m.PhoneNumber == dto.PhoneNumber || m.NationalId == dto.NationalId);

			if (caseDb == null)
				return null;

			if (caseDb.PhoneNumber == dto.PhoneNumber)
				return new BadRequest(nameof(dto.PhoneNumber), "Phone number already exists");
			else
				return new BadRequest(nameof(dto.NationalId), "National id already exists");
		}
	}
}
