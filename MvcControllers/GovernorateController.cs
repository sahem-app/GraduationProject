using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Utilities.General;
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
            var Count = governorates.Count();
            TempData["count"] = Count;
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GovernorateVM model)
        {
            if (!ModelState.IsValid)
                return View();

            model.Name = model.Name.Trim();
            if (await _context.Governorates.AnyAsync(g => g.Name == model.Name))
            {
                ModelState.AddModelError("", $"{model.Name} governorate already exists");
                return View();
            }

            var governorate = new Governorate()
            {
                Name = model.Name,
                Name_AR = model.Name_AR,
                Image = FormFileHandler.ConvertToBytes(model.Image)
            };

            await _context.Governorates.AddAsync(governorate);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Governorate added successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var governorate = await _context.Governorates.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);

            var goverVM = new GovernorateVM()
            {
                Name = governorate.Name,
                Name_AR = governorate.Name_AR,
            };
            return governorate == null ? NotFound() : View(goverVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GovernorateVM model)
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
            governorate.Name_AR = model.Name_AR;
            governorate.Image = FormFileHandler.ConvertToBytes(model.Image);

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

            try
            {
                _context.Governorates.Remove(new Governorate(id));
                await _context.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Governorate deleted successfully");
            }
            catch (Exception)
            { 
                _toast.AddErrorToastMessage("can not delete this governorate"); 
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
