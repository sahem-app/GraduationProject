using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class MessageType
    {
        public Enums.MessageType Id { get; set; }

        [Required, MaxLength(20), Column(TypeName = "varchar")]
        public string Name { get; set; }
    }
}
