using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models.CaseProperties
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(50), Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Name_AR { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}
