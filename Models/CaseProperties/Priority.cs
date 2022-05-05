using GraduationProject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models.CaseProperties
{
    public class Priority
    {
        public PriorityType Id { get; set; }

        [Required, MaxLength(50), Column(TypeName = "varchar")]
        public string Name { get; set; }
    }
}
