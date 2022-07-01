using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Models.CaseProperties;
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
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toast;

        public CategoryController(ApplicationDbContext context, IToastNotification toast)
        {
            _context = context;
            _toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pg = 1)
        {
            var Categories = await _context.Categories.AsNoTracking().ToArrayAsync();
            int pageSize = 3;
            if (pg < 1)
                pg = 1;
            var recsCounet = Categories.Count();
            var pager = new Pager(recsCounet, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = Categories.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            var count = Categories.Count();
            TempData["count"] = count;
            //return View(await _context.Categories.AsNoTracking().ToArrayAsync());
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _context.Categories.AddAsync(model);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{model.Name} category added successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(uint id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return category == null ? NotFound() : View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (category == null)
                return NotFound();

            category.Name = model.Name;
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{model.Name} category updated successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage($"{category.Name} category deleted successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
