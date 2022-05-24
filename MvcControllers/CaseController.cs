using GraduationProject.Data;
using GraduationProject.Enums;
using GraduationProject.Models;
using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using GraduationProject.Services;
using GraduationProject.Utilities.General;
using GraduationProject.Utilities.StaticStrings;
using GraduationProject.ViewModels.Cases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NToastNotify;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.MvcControllers
{
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly ISMSService _sMSService;

        public CaseController(ApplicationDbContext context, IToastNotification toastNotification, ISMSService sMSService)
        {
            _context = context;
            _toastNotification = toastNotification;
            _sMSService = sMSService;
        }
     
        [HttpGet]
        public async Task<IActionResult> AcceptedCases(int pg = 1)

        {
            var Case = await _context.Cases.AsNoTracking()
                .Where(m => m.Status.Id == StatusType.Accepted)
                .ToArrayAsync();
            int pageSize = 3;
            if (pg < 1)
                pg = 1;
            var count = Case.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = Case.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;


            TempData["count"] = count;

            return View(data);

        }

        [HttpGet]
        public async Task<IActionResult> PendingCases(int pg = 1)
        {
            var Case = await _context.Cases.AsNoTracking()
              .Where(m => m.Status.Id == StatusType.Submitted)
              .ToArrayAsync();

            int pageSize = 3;
            if (pg < 1)
                pg = 1;
            var count = Case.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = Case.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;


            TempData["count"] = count;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> RejectedCases(int pg = 1)
        {
            var Case = await _context.Cases.AsNoTracking()
               .Where(m => m.Status.Id == StatusType.Rejected)
               .ToArrayAsync();

            int pageSize = 3;
            if (pg < 1)
                pg = 1;
            var count = Case.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = Case.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;


            TempData["count"] = count;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CrudCaseVM
            {
                SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = e, Name = e.ToString() }),
                Status = Enum.GetValues<StatusType>().Select(e => new Status { Id = e, Name = e.ToString() }),
                Gender = Enum.GetValues<GenderType>().Select(e => new Gender { Id = e, Name = e.ToString() }),
                Periods = Enum.GetValues<PeriodType>().Select(e => new Period { Id = e, Name = e.ToString() }),
                Priorities = Enum.GetValues<PriorityType>().Select(e => new Priority { Id = e, Name = e.ToString() }),
                Relationships = Enum.GetValues<RelationshipType>().Select(e => new Relationship { Id = e, Name = e.ToString() }),
                Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync(),
                Categories = await _context.Categories.AsNoTracking().ToArrayAsync(),
                //GeoLocation = await _context.GeoLocations.AsNoTracking().ToArrayAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrudCaseVM model)
        {
            model.SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = e, Name = e.ToString() });
            model.Status = Enum.GetValues<StatusType>().Select(e => new Status { Id = e, Name = e.ToString() });
            model.Gender = Enum.GetValues<GenderType>().Select(e => new Gender { Id = e, Name = e.ToString() });
            model.Periods = Enum.GetValues<PeriodType>().Select(e => new Period { Id = e, Name = e.ToString() });
            model.Priorities = Enum.GetValues<PriorityType>().Select(e => new Priority { Id = e, Name = e.ToString() });
            model.Relationships = Enum.GetValues<RelationshipType>().Select(e => new Relationship { Id = e, Name = e.ToString() });
            model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
            model.Categories = await _context.Categories.AsNoTracking().ToArrayAsync();
            //model.GeoLocation = await _context.GeoLocations.AsNoTracking().ToArrayAsync();
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
                RelationshipId = model.RelationshipId,
                PeriodId = model.PeriodId,
                CategoryId = model.CategoryId,
                PriorityId = model.PriorityId,
                GeoLocation = new GeoLocation
                {
                    Location = new Point(model.Longitude, model.Latitude) { SRID = 4326 },
                    Details = model.Details
                },
                StatusId = model.StatusId,
                GenderId = model.GenderId,
                RegionId = model.RegionId,
                MediatorId = 1,
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
            try
            {
                //sms message
                var result = _sMSService.Send(Case.PhoneNumber, $"{Case.Name} You have been added in sahem application");

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    return BadRequest(result.ErrorMessage);

                _toastNotification.AddInfoToastMessage("The sms message has been sent successfully");
            }
            catch (Exception ex) { }
            

            if (model.StatusId == StatusType.Accepted)
                return RedirectToAction(nameof(AcceptedCases));
            else if (model.StatusId == StatusType.Rejected)
                return RedirectToAction(nameof(RejectedCases));

            return RedirectToAction(nameof(PendingCases));
        }

        [HttpGet]
        public async Task<IActionResult> Details(uint id)
        {
            var Case = await _context.Cases.Include(c => c.GeoLocation).AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CaseVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Status = c.Status.Name,
                    NationalId = c.NationalId,
                    SocialStatus = c.SocialStatus.Name,
                    Address = c.Address,
                    Adults = c.Adults,
                    Children = c.Children,
                    Longitude = c.GeoLocation.Location.Coordinate.X,
                    Latitude = c.GeoLocation.Location.Coordinate.Y,
                    Details = c.GeoLocation.Details,
                    NeededMoneyAmount = c.NeededMoneyAmount,
                    Title = c.Title,
                    Relationship = c.Relationship.Name,
                    Gender = c.Gender.Name,
                    Story = c.Story,
                    BirthDate = c.BirthDate,
                    PaymentDate = c.PaymentDate,
                    Category = c.Category.Name,
                    Priority = c.Priority.Name,
                    Period = c.Period.Name,
                    NationalIdImage = c.NationalIdImage,
                    Mediator = c.Mediator.Name,
                    Region = c.Region.Name,
                    City = c.Region.City.Name,
                    Governorate = c.Region.City.Governorate.Name,
                    ReviewsAboutMe = c.CaseReviews.Select(r => new CaseReviewVM
                    {
                        Name = r.Mediator.Name,
                        Description = r.Description,
                        IsWorthy = r.IsWorthy,
                        ReviewDate = r.DateReviewed,
                        Image = r.Mediator.ProfileImage
                    }).ToArray()
                }).FirstOrDefaultAsync();
            //.Include(c => c.Category.Name)
            //.Include(p => p.Priority.Name)
            //.Include(p => p.Period.Name)
            //.Include(r => r.Relationship.Name)
            //.Include(m => m.Mediator.Name)
            //.Include(s => s.Status.Name)
            //.Include(s => s.SocialStatus.Name)
            //.Include(r => r.Region.Name)
            //.Include(c => c.Region.City.Name)
            //.Include(g => g.Region.City.Governorate.Name)
            //.Include(g => g.Gender.Name)

            return Case == null ? NotFound() : View(Case);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var Case = await _context.Cases.Include(c => c.GeoLocation).FirstOrDefaultAsync(m => m.Id == id);
            if (Case == null)
                return NotFound();

            var CaseVM = new CrudCaseVM()
            {
                Name = Case.Name,
                PhoneNumber = Case.PhoneNumber,
                BirthDate = Case.BirthDate,
                PaymentDate = Case.PaymentDate,
                Longitude = Case.GeoLocation.Location.Coordinate.X,
                Latitude = Case.GeoLocation.Location.Coordinate.Y,
                Details = Case.GeoLocation.Details,
                NationalId = Case.NationalId,
                Address = Case.Address,
                Adults = Case.Adults,
                Title = Case.Title,
                PeriodId = Case.PeriodId,
                RelationshipId = Case.RelationshipId,
                Children = Case.Children,
                NeededMoneyAmount = Case.NeededMoneyAmount,
                Story = Case.Story,
                CategoryId = Case.CategoryId,
                PriorityId = Case.PriorityId,
                GenderId = Case.GenderId,
                StatusId = Case.StatusId,
                SocialStatusId = Case.SocialStatusId,
                RegionId = Case.RegionId,
                SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = e, Name = e.ToString() }),
                Status = Enum.GetValues<StatusType>().Select(e => new Status { Id = e, Name = e.ToString() }),
                Gender = Enum.GetValues<GenderType>().Select(e => new Gender { Id = e, Name = e.ToString() }),
                Periods = Enum.GetValues<PeriodType>().Select(e => new Period { Id = e, Name = e.ToString() }),
                Relationships = Enum.GetValues<RelationshipType>().Select(e => new Relationship { Id = e, Name = e.ToString() }),
                Categories = await _context.Categories.AsNoTracking().ToArrayAsync(),
                Priorities = await _context.Priorities.AsNoTracking().ToArrayAsync(),
                Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync()
            };

            CaseVM.Region = await _context.Regions.Include(r => r.City)
                .ThenInclude(c => c.Governorate).FirstOrDefaultAsync(r => r.Id == CaseVM.RegionId);
            return View(CaseVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CrudCaseVM model)
        {
            model.SocialStatus = Enum.GetValues<SocialStatusType>().Select(e => new SocialStatus { Id = e, Name = e.ToString() });
            model.Status = Enum.GetValues<StatusType>().Select(e => new Status { Id = e, Name = e.ToString() });
            model.Gender = Enum.GetValues<GenderType>().Select(e => new Gender { Id = e, Name = e.ToString() });
            model.Periods = Enum.GetValues<PeriodType>().Select(e => new Period { Id = e, Name = e.ToString() });
            model.Priorities = Enum.GetValues<PriorityType>().Select(e => new Priority { Id = e, Name = e.ToString() });
            model.Relationships = Enum.GetValues<RelationshipType>().Select(e => new Relationship { Id = e, Name = e.ToString() });
            model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
            model.Categories = await _context.Categories.AsNoTracking().ToArrayAsync();
            //model.GeoLocation = await _context.GeoLocations.AsNoTracking().ToArrayAsync();


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
            Case.CategoryId = model.CategoryId;
            Case.PriorityId = model.PriorityId;
            Case.PeriodId = model.PeriodId;
            Case.RelationshipId = model.RelationshipId;
            //Case.GeoLocationId = model.GeolocationId;
            Case.GeoLocation = new GeoLocation
            {
                Location = new Point(model.Longitude, model.Latitude) { SRID = 4326 },
                Details = model.Details
            };
            Case.StatusId = model.StatusId;
            Case.GenderId = model.GenderId;
            Case.RegionId = model.RegionId;
            Case.NeededMoneyAmount = model.NeededMoneyAmount;
            Case.Adults = model.Adults;
            Case.Children = model.Children;
            Case.NationalIdImage = FormFileHandler.ConvertToBytes(model.NationalIdImage);
            Case.Story = model.Story;

            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage("Case updated successfully");

            if (model.StatusId == StatusType.Accepted)
                return RedirectToAction(nameof(AcceptedCases));
            else if (model.StatusId == StatusType.Rejected)
                return RedirectToAction(nameof(RejectedCases));

            return RedirectToAction(nameof(PendingCases));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(uint id, string Name)
        {
            var Case = await _context.Cases.FirstOrDefaultAsync(m => m.Id == id);
            if (Case == null)
                return NotFound();

            if (StatusType.Accepted.ToString().ToLower() == Name.ToLower())
            {
                Case.StatusId = StatusType.Accepted;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage($"{Case.Name} status updated successfully");
            }
            else if (StatusType.Rejected.ToString().ToLower() == Name.ToLower())
            {
                Case.StatusId = StatusType.Rejected;
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage($"{Case.Name} status updated successfully");
            }

            if (Case.StatusId == StatusType.Accepted)
                return RedirectToAction(nameof(AcceptedCases));

            return RedirectToAction(nameof(RejectedCases));
        }
    }
}
