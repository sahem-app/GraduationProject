using GraduationProject.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models.Shared
{
    public class SocialStatus
    {
        public SocialStatusType Id { get; set; }

        [Required, MaxLength(50), Column(TypeName = "varchar")]
        public string Name { get; set; }
    }
}
