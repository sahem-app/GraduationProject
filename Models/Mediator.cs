﻿using GraduationProject.Enums;
using GraduationProject.Models.Location;
using GraduationProject.Models.Reviews;
using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Mediator
    {
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required, MaxLength(14), Column(TypeName = "varchar")]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(14), Column(TypeName = "varchar")]
        public string NationalId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [MaxLength(4000)]
        public string Bio { get; set; }

        [MaxLength(250)]
        public string Job { get; set; }

        [MaxLength(4000)]
        public string Address { get; set; }

        [MaxLength(4000), Column(TypeName = "varchar")]
        public string FirebaseToken { get; set; }

        public bool Completed { get; set; }

        [Required, Display(Name = "Profile Image")]
        public byte[] ProfileImage { get; set; }

        [Required, Display(Name = "National ID Image")]
        public byte[] NationalIdImage { get; set; }

        [Column(TypeName = "datetime2(0)")]
        public DateTime DateRegistered { get; private set; } = DateTime.Now;

        public GeoLocation GeoLocation { get; set; }
        public int GeoLocationId { get; set; }

        public Region Region { get; set; }
        public int? RegionId { get; set; }

        public Gender Gender { get; set; }
        public GenderType GenderId { get; set; }

        public SocialStatus SocialStatus { get; set; }
        public SocialStatusType SocialStatusId { get; set; }

        public Status Status { get; set; }
        public StatusType StatusId { get; set; }

        public Locale Locale { get; set; }
        public LocaleType LocaleId { get; set; }

        public ICollection<MediatorReview> ReviewsAboutMe;
        public ICollection<MediatorReview> ReviewsByMe;
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Case> CasesAdded { get; set; }

        public Mediator()
        {

        }

        public Mediator(int id)
        {
            Id = id;
        }
    }
}
