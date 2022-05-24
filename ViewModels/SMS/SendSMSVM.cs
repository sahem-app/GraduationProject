using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels.SMS
{
    public class SendSMSVM
    {
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
