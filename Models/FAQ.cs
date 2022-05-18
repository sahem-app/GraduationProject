using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GraduationProject.Models
{
    public class FAQ
    {
        public int Id { get; set; }

        [Required, MaxLength(500), Column(TypeName = "varchar")]
        public string Title { get; set; }

        [Required, MaxLength(4000), Column(TypeName = "varchar")]
        public string Description { get; set; }
    }
}
