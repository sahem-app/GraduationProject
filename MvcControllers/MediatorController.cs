using System;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Data;
using GraduationProject.Enums;
using GraduationProject.Models;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities.StaticStrings;
using GraduationProject.ViewModels.Mediators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace GraduationProject.MvcControllers
{
	[Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	public class MediatorController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IToastNotification _toast;

		public MediatorController(ApplicationDbContext context, IToastNotification toastNotification)
		{
			_context = context;
			_toast = toastNotification;
		}

		[HttpGet]
		public async Task<IActionResult> Index(StatusType status)
		{
			if (!Enum.GetValues<StatusType>().Contains(status))
				status = StatusType.Accepted;
			TempData["status"] = status;
			return View(await _context.Mediators.AsNoTracking()
							.Where(m => m.StatusId == (byte)status)
							.Select(m => new MediatorVM(m))
							.ToArrayAsync());
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View(new CreateMediatorVM
			{
				SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = (byte)e, Name = e.ToString() }),
				Genders = Enum.GetValues<GenderType>().Select(e => new Gender { Id = (byte)e, Name = e.ToString() }),
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateMediatorVM model)
		{
			model.Genders = Enum.GetValues<GenderType>().Select(e => new Gender { Id = (byte)e, Name = e.ToString() });
			model.SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = (byte)e, Name = e.ToString() });
			if (!ModelState.IsValid)
				return View(model);

			if (await _context.Mediators.AnyAsync(m => m.NationalId == model.NationalId))
			{
				ModelState.AddModelError("", "Mediator with the same national ID already exists");
				return View(model);
			}

			if (await _context.Mediators.AnyAsync(m => m.PhoneNumber == model.PhoneNumber))
			{
				ModelState.AddModelError("", "Mediator with the same phone number already exists");
				return View(model);
			}

			await _context.Mediators.AddAsync(model.ToMediator());
			await _context.SaveChangesAsync();
			_toast.AddSuccessToastMessage("Mediator created successfully");
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(uint id)
		{
			if (!await _context.Mediators.AnyAsync(m => m.Id == id && m.StatusId == (byte)StatusType.Accepted))
				return NotFound();

			var genders = Enum.GetValues<GenderType>().Select(g => new Gender { Id = (byte)g, Name = g.ToString() }).ToArray();
			var socialStatus = Enum.GetValues<SocialStatusType>().Select(ss => new SocialStatus { Id = (byte)ss, Name = ss.ToString() }).ToArray();
			var mediator = await _context.Mediators
				.Select(m => new EditMediatorVM
				{
					Id = m.Id,
					Name = m.Name,
					PhoneNumber = m.PhoneNumber,
					NationalId = m.NationalId,
					Job = m.Job,
					Address = m.Address,
					Bio = m.Bio,
					BirthDate = m.BirthDate,
					GeoLocation = m.GeoLocation,
					ProfilePicture = m.ProfileImage,
					NationalIdImage = m.NationalIdImage,
					GenderId = m.GenderId,
					SocialStatusId = m.SocialStatusId,
					RegionId = m.RegionId,
					Genders = genders,
					SocialStatus = socialStatus,
				})
				.FirstOrDefaultAsync(m => m.Id == id);

			if (mediator.RegionId != null)
				mediator.Region = await _context.Regions.AsNoTrackingWithIdentityResolution()
					.Include(r => r.City)
					.FirstAsync(r => r.Id == mediator.RegionId);

			mediator.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
			return View(mediator);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditMediatorVM model)
		{
			var mediator = await _context.Mediators.FirstOrDefaultAsync(m => m.Id == model.Id && m.StatusId == (byte)StatusType.Accepted);
			if (mediator == null)
				return NotFound();

			if (!ModelState.IsValid)
			{
				model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
				model.Genders = Enum.GetValues<GenderType>().Select(g => new Gender { Id = (byte)g, Name = g.ToString() }).ToArray();
				model.SocialStatus = Enum.GetValues<SocialStatusType>().Select(ss => new SocialStatus { Id = (byte)ss, Name = ss.ToString() }).ToArray();
				model.ProfilePicture = mediator.ProfileImage;
				model.NationalIdImage = mediator.NationalIdImage;
				if (model.RegionId != null)
					model.Region = await _context.Regions.AsNoTrackingWithIdentityResolution()
						.Include(r => r.City)
						.FirstAsync(r => r.Id == model.RegionId);

				return View(model);
			}

			model.UpdateMediator(mediator);
			await _context.SaveChangesAsync();
			_toast.AddSuccessToastMessage("Mediator updated successfully");
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(uint id)
		{
			var mediator = await _context.Mediators
				.Where(m => m.Id == id)
				.Select(m => new MediatorDetailsVM
				{
					Id = m.Id,
					Name = m.Name,
					PhoneNumber = m.PhoneNumber,
					NationalId = m.NationalId,
					BirthDate = m.BirthDate,
					Bio = m.Bio,
					Job = m.Job,
					Address = m.Address,
					ProfileImage = m.ProfileImage,
					NationalIdImage = m.NationalIdImage,
					DateRegistered = m.DateRegistered,
					GeoLocation = m.GeoLocation,
					Governorate = m.Region.City.Governorate.Name,
					City = m.Region.City.Name,
					Region = m.Region.Name,
					Gender = m.Gender.Name,
					SocialStatus = m.SocialStatus.Name,
					Status = (StatusType)m.StatusId,
					ReviewsAboutMe = m.ReviewsAboutMe.Select(r => new MediatorReviewVM
					{
						Name = r.Reviewer.Name,
						IsWorthy = r.IsWorthy,
						ReviewDate = r.DateReviewed,
						Description = r.Description,
						Image = r.Reviewer.ProfileImage
					}).ToArray()
				})
				.FirstOrDefaultAsync();

			return mediator == null ? NotFound() : View(mediator);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Accept(uint id)
		{
			var IsExists = await _context.Mediators
				.AnyAsync(m => m.Id == id && m.StatusId == (byte)StatusType.Submitted);

			if (!IsExists)
				return NotFound();

			var mediator = new Mediator { Id = (int)id };
			_context.Mediators.Attach(mediator);
			mediator.StatusId = (byte)StatusType.Accepted;
			await _context.SaveChangesAsync();
			_toast.AddSuccessToastMessage("Mediator accepted");
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Reject(uint id, string decription)
		{
			var IsExists = await _context.Mediators
				.AnyAsync(m => m.Id == id && m.StatusId == (byte)StatusType.Submitted);

			if (!IsExists)
				return NotFound();

			Console.WriteLine(decription);
			var mediator = new Mediator { Id = (int)id };
			_context.Mediators.Attach(mediator);
			mediator.StatusId = (byte)StatusType.Rejected;
			await _context.SaveChangesAsync();
			_toast.AddSuccessToastMessage("Mediator rejected");
			return RedirectToAction(nameof(Index));
		}
	}
}
