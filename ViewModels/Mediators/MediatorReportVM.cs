using GraduationProject.Enums;
using System;

namespace GraduationProject.ViewModels.Mediators
{
    public class MediatorReportVM
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Job { get; set; }
        public string Address { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Gender { get; set; }
        public string SocialStatus { get; set; }
        public StatusType Status { get; set; }

    }
}
