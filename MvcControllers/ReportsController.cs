using GraduationProject.Data;
using GraduationProject.Enums;
using GraduationProject.ViewModels.Cases;
using GraduationProject.ViewModels.Mediators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.MvcControllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AcceptedCases()
        {
            var result = await _context.Cases.AsNoTracking()
                .Where(x => x.StatusId == StatusType.Accepted)
                .Select(c => new CaseReportVM
                {
                    Name = c.Name,
                    Address = c.Address,
                    Adults = c.Adults,
                    BirthDate = c.BirthDate,
                    Children = c.Children,
                    Gender = c.Gender.Name,
                    NationalId = c.NationalId,
                    NeededMoneyAmount = c.NeededMoneyAmount,
                    PaymentDate = c.PaymentDate,
                    PhoneNumber = c.PhoneNumber,
                    SocialStatus = c.SocialStatus.Name,
                    Status = c.Status.Name
                })
                .ToArrayAsync();

            var count = result.Count();
            TempData["count"] = count;
            return new ViewAsPdf(result);
        }

        [HttpGet]
        public async Task<IActionResult> PendingCases()
        {
            var result = await _context.Cases.AsNoTracking()
                .Where(x => x.StatusId == StatusType.Submitted)
                .Select(c => new CaseReportVM
                {
                    Name = c.Name,
                    Address = c.Address,
                    Adults = c.Adults,
                    BirthDate = c.BirthDate,
                    Children = c.Children,
                    Gender = c.Gender.Name,
                    NationalId = c.NationalId,
                    NeededMoneyAmount = c.NeededMoneyAmount,
                    PaymentDate = c.PaymentDate,
                    PhoneNumber = c.PhoneNumber,
                    SocialStatus = c.SocialStatus.Name,
                    Status = c.Status.Name
                })
                .ToArrayAsync();

            var count = result.Count();
            TempData["count"] = count;
            return new ViewAsPdf(result);
        }

        [HttpGet]
        public async Task<IActionResult> RejectedCases()
        {
            var result = await _context.Cases.AsNoTracking()
                .Where(x => x.StatusId == StatusType.Rejected)
                .Select(c => new CaseReportVM
                {
                    Name = c.Name,
                    Address = c.Address,
                    Adults = c.Adults,
                    BirthDate = c.BirthDate,
                    Children = c.Children,
                    Gender = c.Gender.Name,
                    NationalId = c.NationalId,
                    NeededMoneyAmount = c.NeededMoneyAmount,
                    PaymentDate = c.PaymentDate,
                    PhoneNumber = c.PhoneNumber,
                    SocialStatus = c.SocialStatus.Name,
                    Status = c.Status.Name
                })
                .ToArrayAsync();

            var count = result.Count();
            TempData["count"] = count;
            return new ViewAsPdf(result);
        }

        [HttpGet]
        public async Task<IActionResult> ExportMediators(string status)
        {
            var result = await _context.Mediators.AsNoTracking()
                .Where(x => x.Status.Name.ToLower() == status.ToLower())
                .Select(c => new MediatorReportVM
                {
                    Name = c.Name,
                    Address = c.Address,
                    BirthDate = c.BirthDate,
                    Gender = c.Gender.Name,
                    NationalId = c.NationalId,
                    Job = c.Job,
                    City = c.Region.City.Name,
                    Governorate = c.Region.City.Governorate.Name,
                    Region = c.Region.Name,
                    PhoneNumber = c.PhoneNumber,
                    Status = StatusType.Accepted
                })
                .ToArrayAsync();

            var count = result.Count();
            TempData["count"] = count;
            TempData["status"] = status.ToString();
            return new ViewAsPdf(result);
        }
    }
}
