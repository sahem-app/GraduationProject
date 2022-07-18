﻿using GraduationProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels.Mediators
{
    public class MediatorDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Bio { get; set; }
        public string Job { get; set; }
        public string Address { get; set; }
        public byte[] ProfileImage { get; set; }
        public byte[] NationalIdImage { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.Now;

        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required, MaxLength(4000)]
        public string Details { get; set; }
        //public GeoLocation GeoLocation { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Gender { get; set; }
        public string SocialStatus { get; set; }
        public StatusType Status { get; set; }
        public ICollection<MediatorReviewVM> ReviewsAboutMe;
    }
}
