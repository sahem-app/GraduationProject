using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Utilities.StaticStrings;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.MvcControllers
{
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class RegionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toast;

        public RegionController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toast = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pg = 1)
        {
            var regions = await _context.Regions.AsNoTrackingWithIdentityResolution()
                .Include(r => r.City)
                .ThenInclude(c => c.Governorate)
                .ToArrayAsync();
            int pageSize = 3;
            if (pg < 1)
                pg = 1;
            var count = regions.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = regions.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            var Count = regions.Count();
            TempData["count"] = Count;
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new RegionVM
            {
                Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegionVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
                return View(model);
            }

            model.Name = model.Name.Trim();
            if (await _context.Regions.AnyAsync(r => r.Name == model.Name && r.CityId == model.CityId))
            {
                model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
                ModelState.AddModelError("", $"{model.Name} region already exists in this city");
                return View(model);
            }

            var region = new Region
            {
                Name = model.Name,
                Name_AR = model.Name_AR,
                CityId = model.CityId,
            };
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Region added successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var region = await _context.Regions.AsNoTrackingWithIdentityResolution()
                .Include(r => r.City)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (region == null)
                return NotFound();

            var regionVM = new RegionVM
            {
                Id = region.Id,
                Name = region.Name,
                Name_AR = region.Name_AR,
                CityId = region.CityId,
                GovernorateId = region.City.GovernorateId,
                Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync()
            };
            return View(regionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegionVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Name = model.Name.Trim();
            if (await _context.Regions.AnyAsync(r => r.Name == model.Name && r.Name_AR == model.Name_AR && r.CityId == model.CityId))
            {
                model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
                ModelState.AddModelError("", $"{model.Name} region already exists in this city");
                return View(model);
            }

            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == model.Id);
            if (region == null)
                return NotFound();

            region.Name = model.Name;
            region.Name_AR = model.Name_AR;
            region.CityId = model.CityId;
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Region updated successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id)
        {
            if (!await _context.Regions.AnyAsync(r => r.Id == id))
                return NotFound();

            try
            {
                _context.Regions.Remove(new Region(id));
                await _context.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Region deleted successfully");
            }
            catch (Exception)
            {
                _toast.AddErrorToastMessage("can not delete this region");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
