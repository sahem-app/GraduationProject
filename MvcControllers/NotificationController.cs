using GraduationProject.Data;
using GraduationProject.Utilities.General;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.MvcControllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Send(int mediatorid, string message)
        {
            var firebaseToken = _context.Mediators
                .Where(m => m.Id == mediatorid)
                .Select(m => m.FirebaseToken)
                .FirstOrDefault();

            if (firebaseToken == null) return NotFound();

            var handler = new NotificationHandler(message);
            await handler.SendAsync(firebaseToken);
            return View();
        }
    }
}
