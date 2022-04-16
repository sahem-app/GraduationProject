using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GraduationProject.Models.Location;

namespace GraduationProject.ViewModels
{
	public class RegionVM
	{
		public int? Id { get; set; }

		[Required, StringLength(200)]
		public string Name { get; set; }

		[Display(Name = "City")]
		public int CityId { get; set; }
		public IEnumerable<City> City { get; set; }

		[Display(Name = "Governorate")]
		public int GovernorateId { get; set; }
		public IEnumerable<Governorate> governorate { get; set; }
	}
}
