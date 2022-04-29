using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Location;
using GraduationProject.Models.Reviews;
using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraduationProject.Models
{
	public class Case
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(250)]
		public string Name { get; set; }

		[Required]
		[MaxLength(11)]
		[Column(TypeName = "varchar")]
		public string PhoneNumber { get; set; }

		[Required]
		[MaxLength(14)]
		[Column(TypeName = "varchar")]
		[Display(Name = "National Id")]
		public string NationalId { get; set; }

		[Column(TypeName = "date")]
		public DateTime BirthDate { get; set; }

		public byte Adults { get; set; }

		public byte Children { get; set; }

		[Display(Name = "Needed money amount")]
		public int NeededMoneyAmount { get; set; }

		[Column(TypeName = "date")]
		public DateTime PaymentDate { get; set; }

		[Column(TypeName = "date")]
		public DateTime DateRequested { get; private set; } = DateTime.Now;

		[Display(Name ="National card image")]
		public byte[] NationalIdImage { get; set; }

		[MaxLength(4000)]
		public string Address { get; set; }

		[Required, MaxLength(250)]
		public string Title { get; set; }

		[Required, MaxLength(4000)]
		public string Story { get; set; }

		public Period Period { get; set; }
		[Display(Name ="Period")]
		public byte PeriodId { get; set; }

		public Mediator Mediator { get; set; }
		[Display(Name = "Mediator")]
		public int MediatorId { get; set; }

		public Category Category { get; set; }
		[Display(Name = "Category")]
		public byte CategoryId { get; set; }

		public Relationship Relationship { get; set; }
		[Display(Name = "Relationship")]
		public byte RelationshipId { get; set; }

		public Priority Priority { get; set; }
		[Display(Name = "Priority")]
		public byte PriorityId { get; set; }

		public Gender Gender { get; set; }
		[Display(Name = "Gender")]
		public byte GenderId { get; set; }

		public GeoLocation GeoLocation { get; set; }
		[Display(Name = "GeoLocation")]
		public int GeoLocationId { get; set; }

		public SocialStatus SocialStatus { get; set; }
		[Display(Name = "SocialStatus")]
		public byte SocialStatusId { get; set; }

		public Region Region { get; set; }
		[Display(Name = "Region")]
		public int RegionId { get; set; }

		public Status Status { get; set; }
		[Display(Name = "Status")]
		public byte StatusId { get; set; }

		public ICollection<CaseReview> CaseReviews { get; set; }
		public ICollection<Image> Images { get; set; }
	}
}
