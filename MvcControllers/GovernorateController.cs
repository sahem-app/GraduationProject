using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Utilities.StaticStrings;
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
        public async Task<IActionResult> Index(int pg = 1)
        {
            var governorates = await _context.Governorates.AsNoTracking().ToArrayAsync();
            int pageSize = 3;
            if (pg < 1)
                pg = 1;
            var count = governorates.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = governorates.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
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

            model.Name = model.Name.Trim();
            if (await _context.Governorates.AnyAsync(g => g.Name == model.Name))
            {
                ModelState.AddModelError("", $"{model.Name} governorate already exists");
                return View();
            }

            await _context.Governorates.AddAsync(model);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Governorate added successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var governorate = await _context.Governorates.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
            return governorate == null ? NotFound() : View(governorate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Governorate model)
        {
            if (!ModelState.IsValid)
                return View();

            model.Name = model.Name.Trim();
            if (await _context.Governorates.AnyAsync(g => g.Name == model.Name))
            {
                ModelState.AddModelError("", $"{model.Name} governorate already exists");
                return View();
            }

            var governorate = await _context.Governorates.FirstOrDefaultAsync(g => g.Id == model.Id);
            if (governorate == null)
                return NotFound();

            governorate.Name = model.Name;
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Governorate updated successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id)
        {
            if (!await _context.Governorates.AnyAsync(g => g.Id == id))
                return NotFound();

            _context.Governorates.Remove(new Governorate(id));
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Governorate deleted successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
