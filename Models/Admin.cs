using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraduationProject.Models
{
	public class Admin
	{
		public int Id { get; set; }

		[Required, MaxLength(250)]
		public string Name { get; set; }

		[Required, MaxLength(11), Column(TypeName = "varchar")]
		public string PhoneNumber { get; set; }

		[Required, MaxLength(4000)]
		public string Email { get; set; }

		[Required, MaxLength(8000), Column(TypeName = "varchar")]
		public string PasswordHash { get; set; }

		public bool Locked { get; set; }

		public Gender Gender { get; set; }
		public byte GenderId { get; set; }

		public Status Status { get; set; }
		public byte StatusId { get; set; }
	}
}
