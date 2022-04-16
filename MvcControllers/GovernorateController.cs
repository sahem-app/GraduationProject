using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Utilities.StaticStrings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Threading.Tasks;

namespace GraduationProjectMVC.Controllers
{
	[Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	public class GovernorateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toast;

        public GovernorateController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toast = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			return View(await _context.Governorates.AsNoTracking().ToArrayAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Governorate model)
        {
            if (!ModelState.IsValid)
                return View();

            await _context.Governorates.AddAsync(model);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{model.Name} governorate added successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var governorate = await _context.Governorates.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return governorate == null ? NotFound() : View(governorate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Governorate model)
        {
            if (!ModelState.IsValid)
                return View();

            if (model.Id <= 0)
                return BadRequest();

            var governorate = await _context.Governorates.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (governorate == null)
                return NotFound();

            governorate.Name = model.Name;
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{model.Name} governorate updated Successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(uint id)
        {
            var governorate = await _context.Governorates.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return governorate == null ? NotFound() : View(governorate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id)
        {
            var governorate = await _context.Governorates.FirstOrDefaultAsync(m => m.Id == id);
            if (governorate == null)
                return NotFound();

            _context.Governorates.Remove(governorate);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{governorate.Name} governorate Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
