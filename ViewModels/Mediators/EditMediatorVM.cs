using GraduationProject.Enums;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities.CustomAttributes;
using GraduationProject.Utilities.General;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels.Mediators
{
    public class EditMediatorVM
    {
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required, MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(14)]
        public string NationalId { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(4000)]
        public string Bio { get; set; }

        [MaxLength(250)]
        public string Job { get; set; }

        [MaxLength(4000)]
        public string Address { get; set; }

        public byte[] ProfilePicture { get; set; }

        public byte[] NationalIdImage { get; set; }

        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required, MaxLength(4000)]
        public string Details { get; set; }

        public Region Region { get; set; }
        public int? RegionId { get; set; }

        public GenderType GenderId { get; set; }

        public SocialStatusType SocialStatusId { get; set; }

        [ImageFile(MaxSize = 1024 * 1024)]
        [Display(Name = "National Id Image")]
        public IFormFile NationalIdImageFile { get; set; }

        [ImageFile(MaxSize = 1024 * 1024)]
        [Display(Name = "Profile Image")]
        public IFormFile ProfilePictureFile { get; set; }

        public IEnumerable<Gender> Genders { get; set; }

        public IEnumerable<SocialStatus> SocialStatus { get; set; }

        public IEnumerable<Governorate> Governorates { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Region> Regions { get; set; }

        public void UpdateMediator(Mediator mediator)
        {
            mediator.Name = Name;
            mediator.PhoneNumber = PhoneNumber;
            mediator.Address = Address;
            mediator.NationalId = NationalId;
            mediator.BirthDate = BirthDate;
            mediator.GeoLocation = new GeoLocation
            {
                Location = new Point(Longitude, Latitude) { SRID = 4326 },
                Details = Details
            };
            mediator.Job = Job;
            mediator.SocialStatusId = SocialStatusId;
            mediator.Bio = Bio;
            mediator.GenderId = GenderId;
            mediator.RegionId = RegionId;

            if (NationalIdImageFile != null)
                mediator.NationalIdImage = FormFileHandler.ConvertToBytes(NationalIdImageFile);

            if (ProfilePictureFile != null)
                mediator.ProfileImage = FormFileHandler.ConvertToBytes(ProfilePictureFile);
        }
    }
}
