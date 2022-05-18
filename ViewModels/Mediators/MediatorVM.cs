using GraduationProject.Enums;
using GraduationProject.Models;
using System.ComponentModel;

namespace GraduationProject.ViewModels.Mediators
{
    public class MediatorVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("National ID")]
        public string NationalId { get; set; }

        public StatusType Status { get; set; }

        public byte[] Image { get; set; }

        public MediatorVM(Mediator mediator)
        {
            Id = mediator.Id;
            Name = mediator.Name;
            PhoneNumber = mediator.PhoneNumber;
            NationalId = mediator.NationalId;
            Status = (StatusType)mediator.StatusId;
            Image = mediator.ProfileImage;
        }
    }
}
