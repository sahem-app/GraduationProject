using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities;
using GraduationProject.Utilities.StaticStrings;
using GraduationProject.ViewModels.Cases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.MvcControllers
{
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
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
        public async Task<IActionResult> AcceptedCases()
        {
            return View(await _context.Cases.AsNoTracking()
                .Where(m => m.Status.Id == (byte)StatusType.Accepted)
                .ToArrayAsync());
        }

        [HttpGet]
        public async Task<IActionResult> PendingCases()
        {
            return View(await _context.Cases.AsNoTracking()
                .Where(m => m.StatusId == (byte)StatusType.Submitted)
                .ToArrayAsync());
        }

        [HttpGet]
        public async Task<IActionResult> RejectedCases()
        {
            return View(await _context.Cases.AsNoTracking()
                .Where(m => m.Status.Id == (byte)StatusType.Rejected)
                .ToArrayAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CrudCaseVM
            {
                SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = (byte)e, Name = e.ToString() }),
                Status = Enum.GetValues<StatusType>().Select(e => new Status { Id = (byte)e, Name = e.ToString() }),
                Gender = Enum.GetValues<GenderType>().Select(e => new Gender { Id = (byte)e, Name = e.ToString() }),
                Periods = Enum.GetValues<PeriodType>().Select(e => new Period { Id = (byte)e, Name = e.ToString() }),
                Priorities = Enum.GetValues<PriorityType>().Select(e => new Priority { Id = (byte)e, Name = e.ToString() }),
                Relationships = Enum.GetValues<RelationshipType>().Select(e => new Relationship { Id = (byte)e, Name = e.ToString() }),
                Region = await _context.Regions.AsNoTracking().ToArrayAsync(),
                cities = await _context.Cities.AsNoTracking().ToArrayAsync(),
                Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync(),
                Categories = await _context.Categories.AsNoTracking().ToArrayAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrudCaseVM model)
        {
            model.SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = (byte)e, Name = e.ToString() });
            model.Status = Enum.GetValues<StatusType>().Select(e => new Status { Id = (byte)e, Name = e.ToString() });
            model.Gender = Enum.GetValues<GenderType>().Select(e => new Gender { Id = (byte)e, Name = e.ToString() });
            model.Periods = Enum.GetValues<PeriodType>().Select(e => new Period { Id = (byte)e, Name = e.ToString() });
            model.Priorities = Enum.GetValues<PriorityType>().Select(e => new Priority { Id = (byte)e, Name = e.ToString() });
            model.Relationships = Enum.GetValues<RelationshipType>().Select(e => new Relationship { Id = (byte)e, Name = e.ToString() });
            //model.Region = await _context.Regions.AsNoTracking().ToArrayAsync();
            model.cities = await _context.Cities.AsNoTracking().ToArrayAsync();
            model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
            model.Categories = await _context.Categories.AsNoTracking().ToArrayAsync();
			model.cities = await _context.Cities.AsNoTracking().ToArrayAsync();
            model.MediatorId = 3;
            if (!ModelState.IsValid)
                return View(model);

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
                MediatorId = model.MediatorId,
                RelationshipId = (byte)model.RelationshipId,
                PeriodId = (byte)model.PeriodId,
                CategoryId = (byte)model.CategoryId,
                PriorityId = model.PriorityId,
                GeoLocation = model.GeoLocation,
                StatusId = model.StatusId,
                GenderId = model.GenderId,
                RegionId = model.RegionId,
                NeededMoneyAmount = model.NeededMoneyAmount,
                Adults = model.Adults,
                Children = model.Children,
                NationalIdImage = FormFileHandler.ConvertToBytes(model.NationalIdImage),
                Story = model.Story
            };
            if (await _context.Cases.AnyAsync(c => c.NationalId == model.NationalId))
            {
                ModelState.AddModelError(string.Empty, "Case with the same national ID already exists");
                return View(model);
            }

            if (await _context.Cases.AnyAsync(c => c.PhoneNumber == model.PhoneNumber))
            {
                ModelState.AddModelError(string.Empty, "Case with the same phone number already exists");
                return View(model);
            }

            await _context.Cases.AddAsync(Case);
            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Case created successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetRegion(int id)
        {
            List<Region> Regions = new List<Region>();
            Regions = await _context.Regions.AsNoTracking().Where(m => m.CityId == id).ToListAsync();
            return Json(new SelectList(Regions, "Id", "Name"));
        }

        [HttpGet]
        public async Task<IActionResult> Details(uint id)
        {
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
        public async Task<IActionResult> Edit(uint id)
        {
            var Case = await _context.Cases.FirstOrDefaultAsync(m => m.Id == id);
            if (Case == null)
                return NotFound();

            var CaseVM = new CrudCaseVM()
            {
                Name = Case.Name,
                PhoneNumber = Case.PhoneNumber,
                BirthDate = Case.BirthDate,
                PaymentDate = Case.PaymentDate,
                GeoLocation = Case.GeoLocation,
                NationalId = Case.NationalId,
                Address = Case.Address,
                Adults = Case.Adults,
                Title = Case.Title,
                PeriodId = Case.PeriodId,
                RelationshipId = Case.RelationshipId,
                MediatorId = Case.MediatorId,
                Children = Case.Children,
                NeededMoneyAmount = Case.NeededMoneyAmount,
                Story = Case.Story,
                CategoryId = Case.CategoryId,
                PriorityId = Case.PriorityId,
                GenderId = Case.GenderId,
                StatusId = Case.StatusId,
                SocialStatusId = Case.SocialStatusId,
                RegionId = Case.RegionId,
                SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = (byte)e, Name = e.ToString() }),
                Status = Enum.GetValues<StatusType>().Select(e => new Status { Id = (byte)e, Name = e.ToString() }),
                Gender = Enum.GetValues<GenderType>().Select(e => new Gender { Id = (byte)e, Name = e.ToString() }),
                Periods = Enum.GetValues<PeriodType>().Select(e => new Period { Id = (byte)e, Name = e.ToString() }),
                Relationships = Enum.GetValues<RelationshipType>().Select(e => new Relationship { Id = (byte)e, Name = e.ToString() }),
                Categories = await _context.Categories.AsNoTracking().ToArrayAsync(),
                Priorities = await _context.Priorities.AsNoTracking().ToArrayAsync(),
                Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync(),
                //Region = await _context.Regions.AsNoTracking().ToArrayAsync(),
                cities = await _context.Cities.AsNoTracking().ToArrayAsync()
            };
            return View(CaseVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CrudCaseVM model)
        {
            model.SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = (byte)e, Name = e.ToString() });
            model.Status = Enum.GetValues<StatusType>().Select(e => new Status { Id = (byte)e, Name = e.ToString() });
            model.Gender = Enum.GetValues<GenderType>().Select(e => new Gender { Id = (byte)e, Name = e.ToString() });
            model.Periods = Enum.GetValues<PeriodType>().Select(e => new Period { Id = (byte)e, Name = e.ToString() });
            model.Priorities = Enum.GetValues<PriorityType>().Select(e => new Priority { Id = (byte)e, Name = e.ToString() });
            model.Relationships = Enum.GetValues<RelationshipType>().Select(e => new Relationship { Id = (byte)e, Name = e.ToString() });
            model.Region = await _context.Regions.AsNoTracking().ToArrayAsync();
            model.cities = await _context.Cities.AsNoTracking().ToArrayAsync();
            model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
            model.Categories = await _context.Categories.AsNoTracking().ToArrayAsync();

            if (!ModelState.IsValid)
                return View(model);

            var Case = await _context.Cases.FindAsync(model.Id);

            if (Case == null)
                return NotFound();

            Case.Name = model.Name;
            Case.PhoneNumber = model.PhoneNumber;
            Case.Address = model.Address;
            Case.Title = model.Title;
            Case.NationalId = model.NationalId;
            Case.BirthDate = model.BirthDate;
            Case.PaymentDate = model.PaymentDate;
            Case.SocialStatusId = model.SocialStatusId;
            Case.CategoryId = (byte)model.CategoryId;
            Case.PriorityId = model.PriorityId;
            Case.PeriodId = (byte)model.PeriodId;
            Case.RelationshipId = (byte)model.RelationshipId;
            Case.GeoLocation = model.GeoLocation;
            Case.StatusId = model.StatusId;
            Case.GenderId = model.GenderId;
            Case.RegionId = model.RegionId;
            Case.NeededMoneyAmount = model.NeededMoneyAmount;
            Case.Adults = model.Adults;
            Case.Children = model.Children;
            Case.NationalIdImage = FormFileHandler.ConvertToBytes(model.NationalIdImage);
            Case.Story = model.Story;

            if (await _context.Cases.AnyAsync(c => c.NationalId == model.NationalId))
            {
                ModelState.AddModelError(string.Empty, "Case with the same national ID already exists");
                return View(model);
            }

            if (await _context.Cases.AnyAsync(c => c.PhoneNumber == model.PhoneNumber))
            {
                ModelState.AddModelError(string.Empty, "Case with the same phone number already exists");
                return View(model);
            }
            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Case updated successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(uint id, string Name)
        { 
            var Case = await _context.Cases.FirstOrDefaultAsync(m=>m.Id == id);
            if (Case == null)
                return NotFound();

            if (StatusType.Accepted.ToString().ToLower() == Name.ToLower())
            {
                Case.StatusId = (byte)StatusType.Accepted;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage($"{Case.Name} status updated successfully");
            }
            else if (StatusType.Rejected.ToString().ToLower() == Name.ToLower())
            {
                Case.StatusId = (byte)StatusType.Rejected;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage($"{Case.Name} status updated successfully");
            }
            
            if(Case.StatusId == (byte)StatusType.Accepted)
                return RedirectToAction(nameof(AcceptedCases));
          
            return RedirectToAction(nameof(RejectedCases));
        }
    }
}
