using GraduationProject.Utilities.CustomAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.ViewModels.Cases
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required, MaxLength(50), Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Name_AR { get; set; }

        [ImageFile(MaxSize = 1024 * 1024), Required]
        public IFormFile Image { get; set; }
    }
}
