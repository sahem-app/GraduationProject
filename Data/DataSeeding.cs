using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities.StaticStrings;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data
{
    public static class DataSeeding
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Gender>().HasData(StaticValues.Genders());
            builder.Entity<Status>().HasData(StaticValues.Status());
            builder.Entity<SocialStatus>().HasData(StaticValues.SocialStatus());
            builder.Entity<Locale>().HasData(StaticValues.Locales());
            builder.Entity<Period>().HasData(StaticValues.Periods());
            builder.Entity<Priority>().HasData(StaticValues.Priorities());
            builder.Entity<Relationship>().HasData(StaticValues.Relationships());
            builder.Entity<Models.NotificationType>().HasData(StaticValues.NotificationTypes());
            builder.Entity<Models.MessageType>().HasData(StaticValues.MessageTypes());
        }
    }
}
