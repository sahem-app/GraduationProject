using GraduationProject.Enums;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities.CustomAttributes;
using GraduationProject.Utilities.General;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels.Mediators
{
    public class CreateMediatorVM
    {
        [Required, StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Phone number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "National ID")]

        [Required, MaxLength(14, ErrorMessage = "National ID must be 14 digits")]
        public string NationalId { get; set; }

        public GenderType GenderId { get; set; }

        public SocialStatusType SocialStatusId { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required, MaxLength(4000)]
        public string Details { get; set; }

        //[Display(Name = "Geolocation")]
        //public int GeolocationId { get; set; }
        //public IEnumerable<GeoLocation> GeoLocation { get; set; }

        [Required]
        [ImageFile(MaxSize = 1024 * 1024)]
        [Display(Name = "National ID Image")]
        public IFormFile NationalIdImage { get; set; }

        [Required]
        [ImageFile(MaxSize = 1024 * 1024)]
        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; }

        public IEnumerable<Gender> Genders { get; set; }

        public IEnumerable<SocialStatus> SocialStatus { get; set; }

        public Mediator ToMediator()
        {
            return new Mediator
            {
                Name = Name,
                PhoneNumber = PhoneNumber.ToString(),
                NationalId = NationalId,
                GeoLocation = new GeoLocation
                {
                    Location = new Point(Longitude, Latitude) { SRID = 4326 },
                    Details = Details
                },
                NationalIdImage = FormFileHandler.ConvertToBytes(NationalIdImage),
                ProfileImage = FormFileHandler.ConvertToBytes(ProfileImage),
                GenderId = GenderId,
                SocialStatusId = SocialStatusId,
                LocaleId = LocaleType.EN,
                StatusId = StatusType.Accepted,
            };
        }
    }
}
