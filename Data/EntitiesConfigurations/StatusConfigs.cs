using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Status = GraduationProject.Models.Shared.Status;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class StatusConfigs : IEntityTypeConfiguration<Status>
	{
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			var data = new[]
			{
				new Status { Id = 1, Name = Utilities.StaticStrings.Status.Pending},
				new Status { Id = 2, Name = Utilities.StaticStrings.Status.Accepted},
				new Status { Id = 3, Name = Utilities.StaticStrings.Status.Rejected},
				new Status { Id = 4, Name = Utilities.StaticStrings.Status.Submitted}
			};

			builder.HasData(data);
		}
	}
}
