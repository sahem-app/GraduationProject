using System;
using System.Linq;
using GraduationProject.Enums;
using GraduationProject.Models.CaseProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
	public class RelationshipConfigs : IEntityTypeConfiguration<Relationship>
	{
		public void Configure(EntityTypeBuilder<Relationship> builder)
		{
			var data = Enum.GetValues<RelationshipType>()
				.Select(e => new Relationship
				{
					Id = (byte)e,
					Name = e.ToString()
				});

			builder.HasData(data);
		}
	}
}
