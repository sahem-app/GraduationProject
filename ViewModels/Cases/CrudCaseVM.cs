using GraduationProject.Enums;
using GraduationProject.Models;
using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities.CustomAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.ViewModels.Cases
{
    public class CrudCaseVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="This field is required")]
        public string Name { get; set; }

        [MaxLength(14)]
        [DisplayName("Phone Number"), Required(ErrorMessage = "This field is required")]
        public string PhoneNumber { get; set; }

        [MaxLength(14,ErrorMessage ="The national id should not be more than 14 digits"),MinLength(14, ErrorMessage = "The national id should not be less than 14 digits")]
        [DisplayName("National ID"),Required(ErrorMessage = "This field is required")]
        public string NationalId { get; set; }

        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }

        public byte Adults { get; set; }

        public byte Children { get; set; }

        public int NeededMoneyAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime PaymentDate { get; set; }

        [ImageFile(MaxSize = 1024 * 1024) ,Required, DisplayName("National Card Image")]
        public IFormFile NationalIdImage { get; set; }

        [StringLength(2000)]
        public string Address { get; set; }

        public string Title { get; set; }

        [Required(ErrorMessage ="This field  is required"), StringLength(4000)]
        public string Story { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        [DisplayName("Category"), Required(ErrorMessage = "This field is required")]
        public int CategoryId { get; set; }

        public IEnumerable<Priority> Priorities { get; set; }
        [DisplayName("Case Priority"), Required(ErrorMessage = "This field is required")]
        public byte PriorityId { get; set; }

        public IEnumerable<Period> Periods { get; set; }
        [DisplayName("Period"), Required(ErrorMessage = "This field is required")]
        public int PeriodId { get; set; }

        public IEnumerable<Gender> Gender { get; set; }
        [DisplayName("Gender"), Required(ErrorMessage = "This field is required")]
        public byte GenderId { get; set; }

        public IEnumerable<SocialStatus> SocialStatus { get; set; }
        [DisplayName("Social Status"), Required(ErrorMessage = "This field is required")]
        public byte SocialStatusId { get; set; }

        public IEnumerable<Governorate> Governorates { get; set; }
        [DisplayName("Governorate"), Required(ErrorMessage = "This field is required")]
        public int GovernorateId { get; set; }

        public IEnumerable<City> cities { get; set; }
        [DisplayName("City"), Required(ErrorMessage = "This field is required")]
        public int CityId { get; set; }
        public IEnumerable<Region> Region { get; set; }
        [DisplayName("Region"), Required(ErrorMessage = "This field is required")]
        public int RegionId { get; set; }
        public GeoLocation GeoLocation { get; set; }

        [DisplayName("Mediator"), Required(ErrorMessage = "This field is required")]
        public int MediatorId { get; set; }

        public IEnumerable<Relationship> Relationships { get; set; }
        [DisplayName("Relationship"), Required(ErrorMessage = "This field is required")]
        public int RelationshipId { get; set; }

        public IEnumerable<Status> Status { get; set; }
        [DisplayName("Status"), Required(ErrorMessage = "This field is required")]
        public byte StatusId { get; set; }
    }
}
