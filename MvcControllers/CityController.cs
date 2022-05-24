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
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.MvcControllers
{
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toast;

        public CityController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toast = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pg=1)
        {
            var cities = await _context.Cities.AsNoTrackingWithIdentityResolution().Include(c => c.Governorate).ToArrayAsync();
            int pageSize = 3;
            if (pg < 1)
                pg = 1;
            var count = cities.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = cities.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            var Count = cities.Count();
            TempData["count"] = count;  
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CityVM
            {
                Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
                return View(model);
            }

            model.Name = model.Name.Trim();
            if (await _context.Cities.AnyAsync(c => c.Name == model.Name && c.GovernorateId == model.GovernorateId))
            {
                model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
                ModelState.AddModelError("", $"{model.Name} city already exists in this governorate");
                return View(model);
            }

            await _context.Cities.AddAsync(new City(model.Name, model.GovernorateId));
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("City added successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var city = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (city == null)
                return NotFound();

            var cityVM = new CityVM
            {
                Id = city.Id,
                Name = city.Name,
                GovernorateId = city.GovernorateId,
                Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync()
            };
            return View(cityVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
                return View(model);
            }

            model.Name = model.Name.Trim();
            if (await _context.Cities.AnyAsync(c => c.Name == model.Name && c.GovernorateId == model.GovernorateId))
            {
                model.Governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
                ModelState.AddModelError("", $"{model.Name} city already exists in this governorate");
                return View(model);
            }

            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (city == null)
                return NotFound();

            city.Name = model.Name;
            city.GovernorateId = model.GovernorateId;
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("City updated successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id)
        {
            if (!await _context.Cities.AnyAsync(c => c.Id == id))
                return NotFound();

            _context.Cities.Remove(new City(id));
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("City deleted successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
