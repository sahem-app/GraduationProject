using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GraduationProject.Enums;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities;
using GraduationProject.Utilities.CustomAttributes;
using Microsoft.AspNetCore.Http;

namespace GraduationProject.ViewModels.Mediators
{
	public class CreateMediatorVM
	{
		[Required, StringLength(250)]
		public string Name { get; set; }

		[Display(Name = "Phone number")]
		[Required, Range(01000000000, 01999999999, ErrorMessage = "Phone number must be 11 digits")]
		public string PhoneNumber { get; set; }

		[Display(Name = "National ID")]
		[Required, Range(10000000000000, 19999999999999, ErrorMessage = "National ID must be 14 digits")]
		public string NationalId { get; set; }

		public byte GenderId { get; set; }

		public byte SocialStatusId { get; set; }

		public GeoLocation GeoLocation { get; set; }

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
				GeoLocation = GeoLocation,
				NationalIdImage = FormFileHandler.ConvertToBytes(NationalIdImage),
				ProfileImage = FormFileHandler.ConvertToBytes(ProfileImage),
				GenderId = GenderId,
				SocialStatusId = SocialStatusId,
				LocaleId = (byte)LocaleType.EN,
				StatusId = (byte)StatusType.Accepted,
			};
		}
	}
}
