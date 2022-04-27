using System;
using System.Linq;
using GraduationProject.Enums;
using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class SocialStatusConfigs : IEntityTypeConfiguration<SocialStatus>
	{
		public void Configure(EntityTypeBuilder<SocialStatus> builder)
		{
			var data = Enum.GetValues<SocialStatusType>()
				.Select(e => new SocialStatus
				{
					Id = (byte)e,
					Name = e.ToString()
				});

			builder.HasData(data);
		}
	}
}
