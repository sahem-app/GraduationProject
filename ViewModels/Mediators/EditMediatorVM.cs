using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GraduationProject.Models;
using GraduationProject.Models.Location;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities;
using GraduationProject.Utilities.CustomAttributes;
using Microsoft.AspNetCore.Http;

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

		public GeoLocation GeoLocation { get; set; }

		public Region Region { get; set; }
		public int? RegionId { get; set; }

		public byte GenderId { get; set; }

		public byte SocialStatusId { get; set; }

		[ImageFile(MaxSize = 1024 * 1024)]
		[Display(Name = "National Id Image")]
		public IFormFile NationalIdImageFile { get; set; }

		[ImageFile(MaxSize = 1024 * 1024)]
		[Display(Name = "Profile Image")]
		public IFormFile ProfilePictureFile { get; set; }

		public IEnumerable<Gender> Genders { get; set; }

		public IEnumerable<SocialStatus> SocialStatus { get; set; }

		public IEnumerable<Governorate> Governorates { get; set; }

		public void UpdateMediator(Mediator mediator)
		{
			mediator.Name = Name;
			mediator.PhoneNumber = PhoneNumber;
			mediator.Address = Address;
			mediator.NationalId = NationalId;
			mediator.BirthDate = BirthDate;
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
