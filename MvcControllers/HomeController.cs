using GraduationProject.Data;
using GraduationProject.Utilities.StaticStrings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GraduationProject.MvcControllers
{
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var numOfCase = _context.Cases.Count();
            TempData["casecount"]= numOfCase;

            var numOfMediator = _context.Mediators.Count();
            TempData["mediatorcount"]=numOfMediator;

            return View();
        }
    }
}
