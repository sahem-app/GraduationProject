using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class AdminConfigs : IEntityTypeConfiguration<Admin>
	{
		public void Configure(EntityTypeBuilder<Admin> builder)
		{
			var data = new[]
			{
				new Admin
				{
					Id = 1,
					Name = "Ahmed Medhat",
					PhoneNumber = "01068218987",
					Email = "admin@sahem.com",
					Locked = false,
					GenderId = 1,
					StatusId = 2,
				}
			};

			var hasher = new PasswordHasher<Admin>();
			data[0].PasswordHash = hasher.HashPassword(data[0], "admin@sahem.com");

			builder.HasData(data);
		}
	}
}
