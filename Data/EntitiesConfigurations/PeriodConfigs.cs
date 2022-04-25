using System;
using System.Linq;
using GraduationProject.Enums;
using GraduationProject.Models.CaseProperties;
using GraduationProject.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class PeriodConfigs : IEntityTypeConfiguration<Period>
	{
		public void Configure(EntityTypeBuilder<Period> builder)
		{
			var data = Enum.GetValues<PeriodType>()
				.Select(e => new Period
				{
					Id = (byte)e,
					Name = e.ToEnumString()
				});

			builder.HasData(data);
		}
	}
}
