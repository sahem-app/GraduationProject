using GraduationProject.Data;
using GraduationProject.Models.Location;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProjectMVC.Controllers
{
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
        public async Task<IActionResult> Index()
        {
			return View(await _context.Regions.AsNoTracking()
				.Include(r => r.City)
				.ThenInclude(c => c.Governorate)
				.ToArrayAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var regionVM = new RegionVM
            {
                //City = await context.Cities.AsNoTracking().ToArrayAsync(),
                governorate = await _context.Governorates.AsNoTracking().ToArrayAsync()
            };
            return View(regionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegionVM model)
        {
            if (!ModelState.IsValid)
                return View();

            var region = new Region
            {
                Name = model.Name,
                CityId = model.CityId,
            };
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{model.Name} region added successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var region = await _context.Regions.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (region == null)
                return NotFound();

            var regionVM = new RegionVM
            {
                Id = region.Id,
                Name = region.Name,
                CityId = region.CityId,
                //City = await context.Cities.AsNoTracking().ToArrayAsync()
                governorate = await _context.Governorates.AsNoTracking().ToArrayAsync()
            };
            return View(regionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegionVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id <= 0)
                return BadRequest();

            var region = await _context.Regions.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (region == null)
                return NotFound();

            region.Name = model.Name;
            region.CityId = model.CityId;
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{model.Name} region updated Successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(uint id)
        {
            var region = await _context.Regions.Include(m => m.City).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return region == null ? NotFound() : View(region);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(m => m.Id == id);
            if (region == null)
                return NotFound();

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{region.Name} Gender Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetCity(uint id)
        {
			IEnumerable<City> cities = await _context.Cities.AsNoTracking().Where(m => m.GovernorateId == id).ToArrayAsync();
            return Json(new SelectList(cities, "Id", "Name"));
        }
    }
}
