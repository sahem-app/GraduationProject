using System;
using System.Linq;
using GraduationProject.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class NotificationTypeConfigs : IEntityTypeConfiguration<Models.NotificationType>
	{
		public void Configure(EntityTypeBuilder<Models.NotificationType> builder)
		{
			var data = Enum.GetValues<NotificationType>()
				.Select(e => new Models.NotificationType
				{
					Id = (byte)e,
					Name = e.ToString()
				});

			builder.HasData(data);
		}
	}
}
