using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace GraduationProject.ViewModels
{
	public class CaseVM
	{
		public int Id { get; set; }

		[Required, MaxLength(200)]
		public string Name { get; set; }

		[Required, MaxLength(11)]
		public string PhoneNumber { get; set; }

		[Required, MaxLength(14), MinLength(14)]
		public string NationalId { get; set; }

		[DisplayName("Birth Date")]
		public DateTime BirthDate { get; set; }

		public byte Adults { get; set; }

		public byte Children { get; set; }

		public int NeededMoneyAmount { get; set; }

		[Column(TypeName = "date")]
		public DateTime PaymentDate { get; set; }

		[Required]
		public IFormFile NationalIdImage { get; set; }

		[MaxLength(2000)]
		public string Address { get; set; }

		public string Title { get; set; }

		[Required, MaxLength(4000)]
		public string Story { get; set; }

		public IEnumerable<Category> CaseCategory { get; set; }
		public int CaseCategoryId { get; set; }

		public IEnumerable<Priority> CasePriority { get; set; }
		public byte CasePriorityId { get; set; }

		public IEnumerable<Gender> Gender { get; set; }
		public byte GenderId { get; set; }

		public IEnumerable<SocialStatus> SocialStatus { get; set; }
		public byte SocialStatusId { get; set; }

		public IEnumerable<City> cities { get; set; }
		public int CityId { get; set; }

		public IEnumerable<Region> Region { get; set; }
		public int RegionId { get; set; }

		public IEnumerable<GeoLocation> GeoLocation { get; set; }
		public int GeoLocationId { get; set; }

		public IEnumerable<Status> Status { get; set; }
		public byte StatusId { get; set; }

		public ICollection<Image> Images { get; set; }
	}
}