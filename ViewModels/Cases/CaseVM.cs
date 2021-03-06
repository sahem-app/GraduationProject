using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels.Cases
{
    public class CaseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime BirthDate { get; set; }
        public byte Adults { get; set; }
        public byte Children { get; set; }
        public int NeededMoneyAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DateRequested { get; private set; } = DateTime.Now;
        public byte[] NationalIdImage { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public string Period { get; set; }
        public string Mediator { get; set; }
        public string Category { get; set; }
        public string Relationship { get; set; }
        public string Priority { get; set; }
        public string Gender { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required, MaxLength(4000)]
        public string Details { get; set; }
        //public GeoLocation GeoLocation { get; set; }
        public string SocialStatus { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Governorate { get; set; }
        public string Status { get; set; }
        public ICollection<CaseReviewVM> ReviewsAboutMe;
    }
}