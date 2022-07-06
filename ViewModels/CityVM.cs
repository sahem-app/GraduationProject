using GraduationProject.Models.Location;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels
{
    public class CityVM
    {
        public int? Id { get; set; }

        [Required, StringLength(250)]
        public string Name { get; set; }

        [Required, MaxLength(250)]
        public string Name_AR { get; set; }

        [DisplayName("Governorate")]
        public int GovernorateId { get; set; }

        public IEnumerable<Governorate> Governorates { get; set; }

    }
}
