﻿using GraduationProject.Models;
using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Location;
using GraduationProject.Models.Reviews;
using GraduationProject.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Mediator> Mediators { get; set; }
        public DbSet<Casepayment> Casepayment { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Locale> Locales { get; set; }
        public DbSet<SocialStatus> SocialStatus { get; set; }
        public DbSet<GeoLocation> GeoLocations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<CaseReview> CaseReviews { get; set; }
        public DbSet<MediatorReview> MediatorReviews { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            DataSeeding.Seed(builder);
            base.OnModelCreating(builder);
        }
    }
}
