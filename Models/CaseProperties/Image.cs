using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models.CaseProperties
{
	public class Image
	{
		public int Id { get; set; }

		[Required]
		public byte[] Data { get; set; }

		public Case Case { get; set; }
		public int CaseId { get; set; }

		public Image()
		{

		}

		public Image(byte[] imageBytes)
		{
			Data = imageBytes;
		}
	}
}
