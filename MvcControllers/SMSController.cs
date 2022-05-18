using GraduationProject.Data;
using GraduationProject.Services;
using GraduationProject.ViewModels.SMS;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace GraduationProject.MvcControllers
{
    public class SMSController : Controller
    {
        private readonly ISMSService _sMSService;
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;

        public SMSController(ISMSService sMSService, ApplicationDbContext context, IToastNotification toastNotification)
        {
            _sMSService = sMSService;
            _context = context;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult Send()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Send(SendSMSVM sms)
        {
            var result = _sMSService.Send(sms.MobileNumber, sms.Body);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            _toastNotification.AddSuccessToastMessage("The message has been sent successfully");
            return RedirectToAction(nameof(Send));
        }
    }
}
