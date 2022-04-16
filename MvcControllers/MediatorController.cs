using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Utilities.StaticStrings;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProjectMVC.Controllers
{
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
        public async Task<IActionResult> GetRegion(int id)
        {
            var regions = await _context.Regions.AsNoTracking().Where(m => m.CityId == id).ToArrayAsync();
            return Json(new SelectList(regions, "Id", "Name"));
        }

        [HttpGet]
        public async Task<IActionResult> RejectedMediators()
        {
            return View(await _context.Mediators.AsNoTracking()
                .Where(m => m.Status.Name == Status.Rejected)
                .ToArrayAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AcceptedMediators()
        {
			return View(await _context.Mediators.AsNoTracking()
				.Where(m => m.Status.Name == Status.Accepted)
				.ToArrayAsync());
        }

		[HttpGet]
		public async Task<IActionResult> PendingMediators()
		{
			return View(await _context.Mediators.AsNoTracking()
				.Where(m => m.Status.Name == Status.Submitted)
				.ToArrayAsync());
		}
		
		[HttpGet]
        public async Task<IActionResult> Create()
        {
			var mediatorViewModel = new MediatorVM
			{
				Cities = await _context.Cities.AsNoTracking().ToArrayAsync(),
				SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync(),
				Status = await _context.Status.AsNoTracking().ToArrayAsync(),
				Genders = await _context.Genders.AsNoTracking().ToArrayAsync(),
				Regions = await _context.Regions.AsNoTracking().ToArrayAsync()
			};
            return View(mediatorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MediatorVM model)
        {
            model.Genders = await _context.Genders.AsNoTracking().ToArrayAsync();
            model.SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync();
            model.Regions = await _context.Regions.AsNoTracking().ToArrayAsync();
            model.Status = await _context.Status.AsNoTracking().ToArrayAsync();
            model.Cities = await _context.Cities.AsNoTracking().ToArrayAsync();

            if (!ModelState.IsValid)
                return View(model);

            var image1 = Request.Form.Files[0];
            var image2 = Request.Form.Files[1];
            var allowedExtention = new List<String> { ".png", ".jpg", ".jpeg", ".jfif" };
            if (!allowedExtention.Contains(Path.GetExtension(image1.FileName.ToLower())) &&
                !allowedExtention.Contains(Path.GetExtension(image2.FileName.ToLower())))
            {
                ModelState.AddModelError(string.Empty, "only .jpg and .png and .jfif images are allowed");
                return View(model);
            }
            //check image size
            if (image1.Length > 1024 * 1024 && image2.Length > 1024 * 1024) // 1 MB
            {
                ModelState.AddModelError(string.Empty, "image cannot be more than 1MB");
                return View(model);
            }

            using var datastream1 = new MemoryStream();
            await image1.CopyToAsync(datastream1);
            using var datastream2 = new MemoryStream();
            await image2.CopyToAsync(datastream2);

            var Mediator = new Mediator
            {
                Name = model.Mediator.Name,
                PhoneNumber = model.Mediator.PhoneNumber,
                Address = model.Mediator.Address,
                NationalId = model.Mediator.NationalId,
                BirthDate = model.Mediator.BirthDate,
                Job = model.Mediator.Job,
                SocialStatusId = model.Mediator.SocialStatusId,
                Bio = model.Mediator.Bio,
                StatusId = model.Mediator.StatusId,
                NationalIdImage = datastream1.ToArray(),
                ProfileImage = datastream2.ToArray(),
                GenderId = model.Mediator.GenderId,
                RegionId = model.Mediator.RegionId

            };
            await _context.Mediators.AddAsync(Mediator);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Mediator Created SuccessFully");
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();

            var mediator = await _context.Mediators.FindAsync(id);
            if (mediator == null)
                return NotFound();

            var MediatorVM = new MediatorVM()
            {
                Mediator = mediator,
                Genders = await _context.Genders.AsNoTracking().ToArrayAsync(),
                SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync(),
                Regions = await _context.Regions.AsNoTracking().ToArrayAsync(),
                Status = await _context.Status.AsNoTracking().ToArrayAsync(),
                Cities = await _context.Cities.AsNoTracking().ToArrayAsync()
            };
            return View(MediatorVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MediatorVM model)
        {
            model.Genders = await _context.Genders.AsNoTracking().ToArrayAsync();
            model.SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync();
            model.Regions = await _context.Regions.AsNoTracking().ToArrayAsync();
            model.Status = await _context.Status.AsNoTracking().ToArrayAsync();
            model.Cities = await _context.Cities.AsNoTracking().ToArrayAsync();

            if (!ModelState.IsValid)
            {

                return View(model);
            }
            var Mediator = await _context.Mediators.FindAsync(model.Mediator.Id);
            if (Mediator == null)
                return NotFound();

            var image1 = Request.Form.Files[0];
            var image2 = Request.Form.Files[1];
            var allowedExtention = new List<String> { ".png", ".jpg", ".jpeg", ".jfif" };
            if (!allowedExtention.Contains(Path.GetExtension(image1.FileName.ToLower())) &&
                !allowedExtention.Contains(Path.GetExtension(image2.FileName.ToLower())))
            {
                ModelState.AddModelError(string.Empty, "only .jpg and .png and .jfif images are allowed");
                return View(model);
            }
            //check image size
            if (image1.Length > 1024 * 1024 && image2.Length > 1024 * 1024) // 1 MB
            {
                ModelState.AddModelError(string.Empty, "image cannot be more than 1MB");
                return View(model);
            }

            using var datastream1 = new MemoryStream();
            await image1.CopyToAsync(datastream1);
            using var datastream2 = new MemoryStream();
            await image2.CopyToAsync(datastream2);

            Mediator.Name = model.Mediator.Name;
            Mediator.PhoneNumber = model.Mediator.PhoneNumber;
            Mediator.Address = model.Mediator.Address;
            Mediator.NationalId = model.Mediator.NationalId;
            Mediator.BirthDate = model.Mediator.BirthDate;
            Mediator.Job = model.Mediator.Job;
            Mediator.SocialStatusId = model.Mediator.SocialStatusId;
            Mediator.Bio = model.Mediator.Bio;
            Mediator.StatusId = model.Mediator.StatusId;
            Mediator.NationalIdImage = datastream1.ToArray();
            Mediator.ProfileImage = datastream2.ToArray();
            Mediator.GenderId = model.Mediator.GenderId;
            Mediator.RegionId = model.Mediator.RegionId;

            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Mediator updated SuccessFully");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id)
        {
            var Mediator = await _context.Mediators.FindAsync(id);
            if (Mediator == null)
                return NotFound();

            _context.Mediators.Remove(Mediator);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Mediator Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(uint id)
        {
            var mediator = await _context.Mediators.AsNoTracking()
                .Include(m => m.Status)
                .Include(m => m.SocialStatus)
                .Include(m => m.Region)
                .Include(m => m.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            return mediator == null ? NotFound() : View(mediator);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, string name)
        {
            if (id <= 0)
                return BadRequest();

            var mediator = await _context.Mediators.Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mediator == null)
                return NotFound();

            if (Status.Accepted == name.ToLower())
            {
                mediator.StatusId = 2;
                _toast.AddSuccessToastMessage($"{mediator.Name} a Status has been changed to {Status.Accepted} successfully");
            }
            else
            {
                mediator.StatusId = 3;
                _toast.AddSuccessToastMessage($"{mediator.Name} a Status has been changed to {Status.Rejected} successfully");
            }
            await _context.SaveChangesAsync();

            if (mediator.StatusId == 2)
                return RedirectToAction(nameof(AcceptedMediators));

            return RedirectToAction(nameof(RejectedMediators));
        }
    }
}
