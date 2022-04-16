using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels
{
	public class SignInVM
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }

		[DisplayName("Remember Me")]
		public bool RememberMe { get; set; }
	}
}
