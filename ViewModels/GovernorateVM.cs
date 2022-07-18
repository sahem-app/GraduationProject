using GraduationProject.Models.Location;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels
{
    public class GovernorateVM
    {
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required, MaxLength(250)]
        public string Name_AR { get; set; }
        [Required]
        public IFormFile Image { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
