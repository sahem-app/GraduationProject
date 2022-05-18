using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels.Cases
{
    public class CaseReportVM
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime BirthDate { get; set; }
        public byte Adults { get; set; }
        public byte Children { get; set; }
        [Display(Name = "Needed Money")]
        public int NeededMoneyAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DateRequested { get; private set; } = DateTime.Now;
        public string Address { get; set; }
        public string Gender { get; set; }
        public string SocialStatus { get; set; }
        public string Status { get; set; }

    }
}
