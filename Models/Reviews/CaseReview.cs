using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models.Reviews
{
    public class CaseReview
    {
        public Case Case { get; set; }
        public int CaseId { get; set; }

        public Mediator Mediator { get; set; }
        public int MediatorId { get; set; }

        public bool IsWorthy { get; set; }

        [Required, MaxLength(4000)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateReviewed { get; private set; } = DateTime.Now;
    }
}
