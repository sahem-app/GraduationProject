﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models.Location
{
    public class Region
    {
        public int Id { get; set; }

        [Required, MaxLength(250), Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Required, MaxLength(250)]
        public string Name_AR { get; set; }

        public City City { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }

        public Region()
        {

        }

        public Region(uint id)
        {
            Id = (int)id;
        }
    }
}
