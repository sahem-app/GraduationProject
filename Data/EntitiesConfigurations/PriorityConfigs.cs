using System;
using System.Linq;
using GraduationProject.Enums;
using GraduationProject.Models.CaseProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class PriorityConfigs : IEntityTypeConfiguration<Priority>
	{
		public void Configure(EntityTypeBuilder<Priority> builder)
		{
			var data = Enum.GetValues<PriorityType>()
				.Select(e => new Priority
				{
					Id = (byte)e,
					Name = e.ToString()
				});

			builder.HasData(data);
		}
	}
}
