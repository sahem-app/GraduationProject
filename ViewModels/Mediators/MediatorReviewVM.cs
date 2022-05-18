using System;

namespace GraduationProject.ViewModels.Mediators
{
    public class MediatorReviewVM
    {
        public string Name { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Description { get; set; }
        public bool IsWorthy { get; set; }
        public byte[] Image { get; set; }
    }
}
