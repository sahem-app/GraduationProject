using System;

namespace GraduationProject.ViewModels.Cases
{
    public class CaseReviewVM
    {
        public string Name { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Description { get; set; }
        public bool IsWorthy { get; set; }
        public byte[] Image { get; set; }
    }
}
