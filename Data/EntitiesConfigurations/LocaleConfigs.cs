using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class LocaleConfigs : IEntityTypeConfiguration<Locale>
	{
		public void Configure(EntityTypeBuilder<Locale> builder)
		{
			var data = new[]
			{
				new Locale { Id = 1, Name = "en"}
			};

			builder.HasData(data);
		}
	}
}
