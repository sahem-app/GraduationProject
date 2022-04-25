﻿using System;
using System.Linq;
using GraduationProject.Enums;
using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class GenderConfigs : IEntityTypeConfiguration<Gender>
	{
		public void Configure(EntityTypeBuilder<Gender> builder)
		{
			var data = Enum.GetValues<GenderType>()
				.Select(e => new Gender
				{
					Id = (byte)e,
					Name = e.ToString()
				});

			builder.HasData(data);
		}
	}
}
