using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace GraduationProject.ViewModels
{
	public class MediatorVM
	{
		public Mediator Mediator { get; set; }
		public IEnumerable<Gender> Genders { get; set; }
		public IEnumerable<Region> Regions { get; set; }
		public IEnumerable<City> Cities { get; set; }
		
		[Display(Name = "City")]
		public int CityId { get; set; }
		public IEnumerable<Status> Status { get; set; }
		public IEnumerable<SocialStatus> SocialStatus { get; set; }

		[Required]
		[Display(Name = "National Id Image")]
		public IFormFile NationalIdImage { get; set; }

		[Required]
		[Display(Name = "Profile Image")]
		public IFormFile ProfileImage { get; set; }





	}
}
