using System.ComponentModel.DataAnnotations;

namespace GraduationProject.DTOs
{
	public class PhoneNumberDto
	{
		[Required, MaxLength(11), MinLength(11), RegularExpression("^[0-9]+$", ErrorMessage = "Phone number must be only numbers")]
		public string PhoneNumber { get; set; }
	}
}
