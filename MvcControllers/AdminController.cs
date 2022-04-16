using GraduationProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.MvcControllers
{
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AdminController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
	}
}
