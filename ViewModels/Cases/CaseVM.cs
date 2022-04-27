using GraduationProject.Enums;
using GraduationProject.Models;
using System.ComponentModel;

namespace GraduationProject.ViewModels.Cases
{
    public class CaseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("National ID")]
        public string NationalId { get; set; }

        public StatusType Status { get; set; }

        [DisplayName("National Card Image")]
        public byte[] NationalIdImage { get; set; }

        public CaseVM(Case Case)
        {
            Id = Case.Id;
            Name = Case.Name;
            PhoneNumber = Case.PhoneNumber;
            NationalIdImage = Case.NationalIdImage;
            Status = (StatusType)Case.StatusId;
            NationalIdImage = Case.NationalIdImage;
        }

    }
}