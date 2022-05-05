﻿using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
    public class CaseConfigs : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.HasIndex(m => m.NationalId).IsUnique();
            builder.HasIndex(m => m.PhoneNumber).IsUnique();

            builder.HasOne(c => c.GeoLocation)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Gender)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Status)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.SocialStatus)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Region)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Priority)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Period)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Relationship)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Mediator)
                .WithMany(m => m.CasesAdded)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
