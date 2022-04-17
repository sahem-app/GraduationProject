using GraduationProject.Data;
using GraduationProject.ViewModels;
using GraduationProject.Models;
using GraduationProject.Utilities.StaticStrings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using GraduationProject.Models.Location;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.MvcControllers
{
    public class CaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;

        public CaseController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            this._context = context;
            this._toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> PendingCases()
        {
            return View(await _context.Cases.AsNoTracking()
                .Where(m => m.Status.Name == Status.Submitted)
                .ToArrayAsync());
        }
        [HttpGet]
        public async Task<IActionResult> AcceptedCases()
        {
            return View(await _context.Cases.AsNoTracking()
                .Where(m => m.Status.Name == Status.Accepted)
                .ToArrayAsync());
        }
        [HttpGet]
        public async Task<IActionResult> RejectedCases()
        {
            return View(await _context.Cases.AsNoTracking()
                .Where(m => m.Status.Name == Status.Rejected)
                .ToArrayAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var CaseVM = new CaseVM()
            {
                CaseCategory = await _context.Categories.AsNoTracking().ToArrayAsync(),
                Mediator = await _context.Mediators.AsNoTracking().ToArrayAsync(),
                Relationship = await _context.Relationships.AsNoTracking().ToArrayAsync(),
                Period = await _context.Periods.AsNoTracking().ToArrayAsync(),
                CasePriority = await _context.Priorities.AsNoTracking().ToArrayAsync(),
                Gender = await _context.Genders.AsNoTracking().ToArrayAsync(),
                GeoLocation = await _context.GeoLocations.AsNoTracking().ToArrayAsync(),
                SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync(),
                Region = await _context.Regions.AsNoTracking().ToArrayAsync(),
                Status = await _context.Status.AsNoTracking().ToArrayAsync(),
                cities = await _context.Cities.AsNoTracking().ToArrayAsync()
            };
            return View(CaseVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaseVM model)
        {
            model.Relationship = await _context.Relationships.AsNoTracking().ToArrayAsync();
            model.CaseCategory = await _context.Categories.AsNoTracking().ToArrayAsync();
            model.Period = await _context.Periods.AsNoTracking().ToArrayAsync();
            model.Mediator = await _context.Mediators.AsNoTracking().ToArrayAsync();
            model.CasePriority = await _context.Priorities.AsNoTracking().ToArrayAsync();
            model.Gender = await _context.Genders.AsNoTracking().ToArrayAsync();
            model.GeoLocation = await _context.GeoLocations.AsNoTracking().ToArrayAsync();
            model.SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync();
            model.Region = await _context.Regions.AsNoTracking().ToArrayAsync();
            model.Status = await _context.Status.AsNoTracking().ToArrayAsync();
            model.cities = await _context.Cities.AsNoTracking().ToArrayAsync();

            if (!ModelState.IsValid)
                return View(model);

            //check on extention image
            var image = Request.Form.Files[0];
            var allowedExtention = new List<String> { ".png", ".jpg", ".jpeg", ".jfif" };
            if (!allowedExtention.Contains(Path.GetExtension(image.FileName.ToLower())))
            {
                ModelState.AddModelError(string.Empty, "only .jpg and .png and .jfif images are allowed");
                return View(model);
            }
            //check image size
            if (image.Length > 1024 * 1024) // 1 MB
            {
                ModelState.AddModelError(string.Empty, "image cannot be more than 1MB");
                return View(model);
            }
            //using var datastream = new MemoryStream();
            //await image.CopyToAsync(datastream);
            var Case = new Case
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Title = model.Title,
                NationalId = model.NationalId,
                BirthDate = model.BirthDate,
                PaymentDate = model.PaymentDate,
                SocialStatusId = model.SocialStatusId,
                CategoryId = model.CaseCategoryId,
                PriorityId = model.CasePriorityId,
                MediatorId = model.MediatorId,
                GeoLocationId = model.GeoLocationId,
                StatusId = model.StatusId,
                GenderId = model.GenderId,
                RegionId = model.RegionId,
                NeededMoneyAmount = model.NeededMoneyAmount,
                Adults = model.Adults,
                Children = model.Children,
                RelationshipId = model.RelationshipId,
                Story = model.Story,
                PeriodId = model.PeriodId
            };
            await Case.SetNationalIdImageAsync(model.NationalIdImage);
            await _context.Cases.AddAsync(Case);
            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Case Created SuccessFully");
            return RedirectToAction(nameof(AcceptedCases));
        }
        [HttpGet]
        public async Task<IActionResult> GetRegion(int id)
        {
            List<Region> Regions = new List<Region>();
            Regions = await _context.Regions.AsNoTracking().Where(m => m.CityId == id).ToListAsync();
            return Json(new SelectList(Regions, "Id", "Name"));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return BadRequest();

            var Case = await _context.Cases.AsNoTracking()
                .Include(m => m.Category)
                .Include(m => m.Priority)
                .Include(m => m.Status)
                .Include(m => m.SocialStatus)
                .Include(m => m.Region)
                .Include(m => m.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            return Case == null ? NotFound() : View(Case);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();

            var Case = await _context.Cases.FindAsync(id);
            if (Case == null)
                return NotFound();

            var CaseVM = new CaseVM()
            {
                Name = Case.Name,
                PhoneNumber = Case.PhoneNumber,
                Address = Case.Address,
                Title = Case.Title,
                NationalId = Case.NationalId,
                BirthDate = Case.BirthDate,
                PaymentDate = Case.PaymentDate,
                SocialStatusId = Case.SocialStatusId,
                CaseCategoryId = Case.CategoryId,
                CasePriorityId = Case.PeriodId,
                MediatorId = Case.MediatorId,
                GeoLocationId = Case.GeoLocationId,
                StatusId = Case.StatusId,
                GenderId = Case.GenderId,
                RegionId = Case.RegionId,
                NeededMoneyAmount = Case.NeededMoneyAmount,
                Adults = Case.Adults,
                Children = Case.Children,
                RelationshipId = Case.RelationshipId,
                Story = Case.Story,
                PeriodId = Case.PeriodId,
                CaseCategory = await _context.Categories.AsNoTracking().ToArrayAsync(),
                Mediator = await _context.Mediators.AsNoTracking().ToArrayAsync(),
                Relationship = await _context.Relationships.AsNoTracking().ToArrayAsync(),
                Period = await _context.Periods.AsNoTracking().ToArrayAsync(),
                CasePriority = await _context.Priorities.AsNoTracking().ToArrayAsync(),
                Gender = await _context.Genders.AsNoTracking().ToArrayAsync(),
                GeoLocation = await _context.GeoLocations.AsNoTracking().ToArrayAsync(),
                SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync(),
                Region = await _context.Regions.AsNoTracking().ToArrayAsync(),
                Status = await _context.Status.AsNoTracking().ToArrayAsync(),
                cities = await _context.Cities.AsNoTracking().ToArrayAsync()
            };
            return View(CaseVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CaseVM model)
        {
            model.Relationship = await _context.Relationships.AsNoTracking().ToArrayAsync();
            model.CaseCategory = await _context.Categories.AsNoTracking().ToArrayAsync();
            model.Period = await _context.Periods.AsNoTracking().ToArrayAsync();
            model.Mediator = await _context.Mediators.AsNoTracking().ToArrayAsync();
            model.CasePriority = await _context.Priorities.AsNoTracking().ToArrayAsync();
            model.Gender = await _context.Genders.AsNoTracking().ToArrayAsync();
            model.GeoLocation = await _context.GeoLocations.AsNoTracking().ToArrayAsync();
            model.SocialStatus = await _context.SocialStatus.AsNoTracking().ToArrayAsync();
            model.Region = await _context.Regions.AsNoTracking().ToArrayAsync();
            model.Status = await _context.Status.AsNoTracking().ToArrayAsync();
            model.cities = await _context.Cities.AsNoTracking().ToArrayAsync();

            if (!ModelState.IsValid)
                return View(model);

            var Case = await _context.Cases.FindAsync(model.Id);
            if (Case == null)
                return NotFound();

            var image = Request.Form.Files[0];

            //check extention
            var allowedExtention = new List<String> { ".png", ".jpg", ".jpeg", ".jfif" };
            if (!allowedExtention.Contains(Path.GetExtension(image.FileName.ToLower())))
            {
                ModelState.AddModelError(string.Empty, "only .jpg and .png and .jfif images are allowed");
                return View(model);
            }

            //check size photo

            var OneMB = 1024 * 1024;
            if (image.Length > OneMB)
            {
                ModelState.AddModelError(string.Empty, "image cannot be more than 1MB");
                return View(model);
            }

            Case.Name = model.Name;
            Case.PhoneNumber = model.PhoneNumber;
            Case.Address = model.Address;
            Case.Title = model.Title;
            Case.NationalId = model.NationalId;
            Case.BirthDate = model.BirthDate;
            Case.PaymentDate = model.PaymentDate;
            Case.SocialStatusId = model.SocialStatusId;
            Case.CategoryId = model.CaseCategoryId;
            Case.PriorityId = model.CasePriorityId;
            Case.StatusId = model.StatusId;
            Case.GenderId = model.GenderId;
            Case.RegionId = model.RegionId;
            Case.NeededMoneyAmount = model.NeededMoneyAmount;
            Case.Adults = model.Adults;
            Case.Children = model.Children;
            Case.Story = model.Story;
            await Case.SetNationalIdImageAsync(model.NationalIdImage);

            await _context.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage($"{Case.Name} information updated SuccessFully");

            if (Case.StatusId == 2)
                return RedirectToAction(nameof(AcceptedCases));
            else if (Case.StatusId == 3)
                return RedirectToAction(nameof(RejectedCases));

            return RedirectToAction(nameof(PendingCases));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var Case = await _context.Cases.FindAsync(id);
            if (Case == null)
                return NotFound();

            _context.Cases.Remove(Case);
            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage($"{Case.Name} information Deleted Successfully");
            return RedirectToAction(nameof(PendingCases));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, string name)
        {
            if (id <= 0)
                return BadRequest();

            var Case = await _context.Cases.Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Case == null)
                return NotFound();

            if (Status.Accepted.ToLower() == name.ToLower())
            {
                Case.StatusId = 2;
                _toastNotification.AddSuccessToastMessage($"{Case.Name} a Status has been changed to {Status.Accepted} successfully");
            }
            else
            {
                Case.StatusId = 3;
                _toastNotification.AddSuccessToastMessage($"{Case.Name} a Status has been changed to {Status.Rejected} successfully");
            }
            await _context.SaveChangesAsync();
            if (Case.StatusId == 2)
            {
                return RedirectToAction(nameof(AcceptedCases));
            }

            return RedirectToAction(nameof(RejectedCases));

        }
    }
}
