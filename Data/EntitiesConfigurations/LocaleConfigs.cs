using System;
using System.Linq;
using GraduationProject.Enums;
using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class LocaleConfigs : IEntityTypeConfiguration<Locale>
	{
		public void Configure(EntityTypeBuilder<Locale> builder)
		{
			var data = Enum.GetValues<LocaleType>()
				.Select(e => new Locale
				{
					Id = (byte)e,
					Name = e.ToString()
				});

			builder.HasData(data);
		}
	}
}
