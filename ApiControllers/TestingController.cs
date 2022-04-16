﻿using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Data;
using GraduationProject.Utilities.CustomApiResponses;
using GraduationProject.Utilities.StaticStrings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.ApiControllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TestingController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public TestingController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet("[action]/{id}")]
		public async Task<IActionResult> GetMediator(uint id)
		{
			var mediator = await _context.Mediators.AsNoTrackingWithIdentityResolution()
				.Include(m => m.Status)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (mediator == null)
				return new BadRequest($"Mediator with id: {id} could not be found");

			return new Success(mediator);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetMediators()
		{
			return new Success(await _context.Mediators.AsNoTrackingWithIdentityResolution()
				.Include(m => m.Status)
				.ToArrayAsync());
		}

		[HttpPost("[action]/{id}")]
		public async Task<IActionResult> AcceptMediator(uint id)
		{
			var mediator = await _context.Mediators.FirstOrDefaultAsync(m => m.Id == id);
			if (mediator == null)
				return new BadRequest($"Mediator with id: {id} could not be found");

			mediator.StatusId = await _context.Status
				.Where(s => s.Name == Status.Accepted)
				.Select(s => s.Id)
				.FirstAsync();

			await _context.SaveChangesAsync();
			return new Success();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> AcceptMediators()
		{
			var mediators = await _context.Mediators.ToArrayAsync();
			if (mediators == null || mediators.Count() <= 0)
				return new Success();

			var acceptedStatusId = await _context.Status.Where(s => s.Name == Status.Accepted).Select(s => s.Id).FirstAsync();
			foreach (var mediator in mediators)
				mediator.StatusId = acceptedStatusId;

			await _context.SaveChangesAsync();
			return new Success();
		}

		[HttpGet("[action]/{id}")]
		public async Task<IActionResult> GetCase(uint id)
		{
			var @case = await _context.Cases.AsNoTrackingWithIdentityResolution()
				.Include(m => m.Status)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (@case == null)
				return new BadRequest($"Case with id: {id} could not be found");

			return new Success(@case);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCases()
		{
			return new Success(await _context.Cases.AsNoTrackingWithIdentityResolution()
				.Include(m => m.Status)
				.ToArrayAsync());
		}
	}
}
