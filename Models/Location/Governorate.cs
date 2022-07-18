﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models.Location
{
    public class Governorate
    {
        public int Id { get; set; }

        [Required, MaxLength(250), Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Required, MaxLength(250)]
        public string Name_AR { get; set; }

        public byte[] Image { get; set; }

        public ICollection<City> Cities { get; set; }

        public Governorate()
        {

        }

        public Governorate(uint id)
        {
            Id = (int)id;
        }
    }
}
