using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GraduationProject.Models.Location;

namespace GraduationProject.ViewModels
{
	public class CityVM
	{
		public int? Id { get; set; }

		[Required, StringLength(250)]
		public string Name { get; set; }

		[DisplayName("Governorate")]
		public int GovernorateId { get; set; }

		public IEnumerable<Governorate> Governorates { get; set; }

	}
}
