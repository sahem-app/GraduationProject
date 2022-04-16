using GraduationProject.Data;
using GraduationProject.Models.Location;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Threading.Tasks;

namespace GraduationProjectMVC.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            var city = await _context.Cities.AsNoTracking().Include(m => m.Governorate).ToArrayAsync();
            return View(city);
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
                return View();

            var city = new City
            {
                GovernorateId = model.GovernorateId,
                Name = model.Name,
            };
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{model.Name} city added successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var city = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
                return NotFound();

            var cityVM = new CityVM
            {
                Id = city.Id,
                Name = city.Name,
                GovernorateId = city.GovernorateId,
                Governorates = await _context.Governorates.ToArrayAsync()
            };
            return View(cityVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityVM model)
        {
            if (!ModelState.IsValid)
                return View();

            if (model.Id <= 0)
                return BadRequest();

            var city = await _context.Cities.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (city == null)
                return NotFound();

            city.Name = model.Name;
            city.GovernorateId = model.GovernorateId;
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{model.Name} city updated Successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(uint id)
        {
            var city = await _context.Cities.AsNoTracking().Include(m => m.Governorate).FirstOrDefaultAsync(m => m.Id == id);
            return city == null ? NotFound() : View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
                return NotFound();

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{city.Name} city Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
