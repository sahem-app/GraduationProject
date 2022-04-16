﻿using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class GenderConfigs : IEntityTypeConfiguration<Gender>
	{
		public void Configure(EntityTypeBuilder<Gender> builder)
		{
			var data = new[]
			{
				new Gender { Id = 1, Name = "Male"},
				new Gender { Id = 2, Name = "Female"}
			};

			builder.HasData(data);
		}
	}
}
