using GraduationProject.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Data.EntitiesConfigurations
{
    public class CityConfigs : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasIndex(g => new { g.Name, g.GovernorateId }).IsUnique();
        }
    }
}
