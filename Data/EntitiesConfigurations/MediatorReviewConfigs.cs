﻿using GraduationProject.Models.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class MediatorReviewConfigs : IEntityTypeConfiguration<MediatorReview>
	{
		public void Configure(EntityTypeBuilder<MediatorReview> builder)
		{
			builder.HasKey(mr => new { mr.RevieweeId, mr.ReviewerId });

			builder.HasOne(m => m.Reviewee)
				.WithMany(m => m.ReviewsAboutMe)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(m => m.Reviewer)
				.WithMany(m => m.ReviewsByMe)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Property(m => m.DateReviewed)
				.HasDefaultValueSql("GETDATE()");
		}
	}
}
