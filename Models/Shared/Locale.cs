using GraduationProject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models.Shared
{
    public class Locale
    {
        public LocaleType Id { get; set; }

        [Required, MaxLength(10), Column(TypeName = "varchar")]
        public string Name { get; set; }
    }
}
