﻿// <auto-generated />
using System;
using GraduationProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GraduationProject.Migrations
{
	[DbContext(typeof(ApplicationDbContext))]
    [Migration("20220416142858_CreateMediatorReviewsTable")]
    partial class CreateMediatorReviewsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GraduationProject.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<byte>("Adults")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<byte>("CategoryId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Children")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("date");

                    b.Property<byte>("GenderId")
                        .HasColumnType("tinyint");

                    b.Property<int>("GeoLocationId")
                        .HasColumnType("int");

                    b.Property<int>("MediatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<byte[]>("NationalIdImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("NeededMoneyAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("date");

                    b.Property<byte>("PeriodId")
                        .HasColumnType("tinyint");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<byte>("PriorityId")
                        .HasColumnType("tinyint");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<byte>("RelationshipId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("SocialStatusId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("StatusId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Story")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GenderId");

                    b.HasIndex("GeoLocationId")
                        .IsUnique();

                    b.HasIndex("MediatorId");

                    b.HasIndex("NationalId")
                        .IsUnique();

                    b.HasIndex("PeriodId");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("PriorityId");

                    b.HasIndex("RegionId");

                    b.HasIndex("RelationshipId");

                    b.HasIndex("SocialStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("GraduationProject.Models.CaseProperties.Category", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Medical"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Poverty"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.CaseProperties.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("GraduationProject.Models.CaseProperties.Period", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("GraduationProject.Models.CaseProperties.Priority", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Priorities");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Urgent"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "High"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "Normal"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.CaseProperties.Relationship", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Relationships");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Self"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Family"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "Neighbor"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.Complain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.HasKey("Id");

                    b.ToTable("Complains");
                });

            modelBuilder.Entity("GraduationProject.Models.Location.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GovernorateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("GovernorateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("GraduationProject.Models.Location.GeoLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(8,6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(9,6)");

                    b.HasKey("Id");

                    b.ToTable("GeoLocations");
                });

            modelBuilder.Entity("GraduationProject.Models.Location.Governorate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Governorates");
                });

            modelBuilder.Entity("GraduationProject.Models.Location.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("GraduationProject.Models.Mediator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Bio")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<string>("FirebaseToken")
                        .HasMaxLength(4000)
                        .HasColumnType("varchar(4000)");

                    b.Property<byte>("GenderId")
                        .HasColumnType("tinyint");

                    b.Property<int>("GeoLocationId")
                        .HasColumnType("int");

                    b.Property<string>("Job")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<byte>("LocaleId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<byte[]>("NationalIdImage")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<byte[]>("ProfileImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("RegionId")
                        .HasColumnType("int");

                    b.Property<byte>("SocialStatusId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("StatusId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("GeoLocationId")
                        .IsUnique();

                    b.HasIndex("LocaleId");

                    b.HasIndex("NationalId")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("RegionId");

                    b.HasIndex("SocialStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Mediators");
                });

            modelBuilder.Entity("GraduationProject.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int>("MediatorId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.HasKey("Id");

                    b.HasIndex("MediatorId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("GraduationProject.Models.Reviews.CaseReview", b =>
                {
                    b.Property<int>("MediatorId")
                        .HasColumnType("int");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsWorthy")
                        .HasColumnType("bit");

                    b.HasKey("MediatorId", "CaseId");

                    b.HasIndex("CaseId");

                    b.ToTable("CaseReviews");
                });

            modelBuilder.Entity("GraduationProject.Models.Reviews.MediatorReview", b =>
                {
                    b.Property<int>("ReviewedId")
                        .HasColumnType("int");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsWorthy")
                        .HasColumnType("bit");

                    b.HasKey("ReviewedId", "ReviewerId");

                    b.HasIndex("ReviewedId")
                        .IsUnique();

                    b.HasIndex("ReviewerId")
                        .IsUnique();

                    b.ToTable("MediatorReviews");
                });

            modelBuilder.Entity("GraduationProject.Models.Shared.Gender", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Genders");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Male"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Female"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.Shared.Locale", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Locales");
                });

            modelBuilder.Entity("GraduationProject.Models.Shared.SocialStatus", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SocialStatus");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Single"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Married"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "Divorced"
                        },
                        new
                        {
                            Id = (byte)4,
                            Name = "Widow"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.Shared.Status", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Accepted"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = (byte)4,
                            Name = "Submitted"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.Case", b =>
                {
                    b.HasOne("GraduationProject.Models.CaseProperties.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Shared.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Location.GeoLocation", "GeoLocation")
                        .WithOne()
                        .HasForeignKey("GraduationProject.Models.Case", "GeoLocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Mediator", "Mediator")
                        .WithMany("CasesAdded")
                        .HasForeignKey("MediatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.CaseProperties.Period", "Period")
                        .WithMany()
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.CaseProperties.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Location.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.CaseProperties.Relationship", "Relationship")
                        .WithMany()
                        .HasForeignKey("RelationshipId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Shared.SocialStatus", "SocialStatus")
                        .WithMany()
                        .HasForeignKey("SocialStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Shared.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Gender");

                    b.Navigation("GeoLocation");

                    b.Navigation("Mediator");

                    b.Navigation("Period");

                    b.Navigation("Priority");

                    b.Navigation("Region");

                    b.Navigation("Relationship");

                    b.Navigation("SocialStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("GraduationProject.Models.CaseProperties.Image", b =>
                {
                    b.HasOne("GraduationProject.Models.Case", "Case")
                        .WithMany("Images")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });

            modelBuilder.Entity("GraduationProject.Models.Location.City", b =>
                {
                    b.HasOne("GraduationProject.Models.Location.Governorate", "Governorate")
                        .WithMany("Cities")
                        .HasForeignKey("GovernorateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("GraduationProject.Models.Location.Region", b =>
                {
                    b.HasOne("GraduationProject.Models.Location.City", "City")
                        .WithMany("Regions")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("GraduationProject.Models.Mediator", b =>
                {
                    b.HasOne("GraduationProject.Models.Shared.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Location.GeoLocation", "GeoLocation")
                        .WithOne()
                        .HasForeignKey("GraduationProject.Models.Mediator", "GeoLocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Shared.Locale", "Locale")
                        .WithMany()
                        .HasForeignKey("LocaleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Location.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GraduationProject.Models.Shared.SocialStatus", "SocialStatus")
                        .WithMany()
                        .HasForeignKey("SocialStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Shared.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("GeoLocation");

                    b.Navigation("Locale");

                    b.Navigation("Region");

                    b.Navigation("SocialStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("GraduationProject.Models.Notification", b =>
                {
                    b.HasOne("GraduationProject.Models.Mediator", "Mediator")
                        .WithMany("Notifications")
                        .HasForeignKey("MediatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mediator");
                });

            modelBuilder.Entity("GraduationProject.Models.Reviews.CaseReview", b =>
                {
                    b.HasOne("GraduationProject.Models.Case", "Case")
                        .WithMany()
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Mediator", "Mediator")
                        .WithMany()
                        .HasForeignKey("MediatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");

                    b.Navigation("Mediator");
                });

            modelBuilder.Entity("GraduationProject.Models.Reviews.MediatorReview", b =>
                {
                    b.HasOne("GraduationProject.Models.Mediator", "Reviewed")
                        .WithOne()
                        .HasForeignKey("GraduationProject.Models.Reviews.MediatorReview", "ReviewedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Mediator", "Reviewer")
                        .WithOne()
                        .HasForeignKey("GraduationProject.Models.Reviews.MediatorReview", "ReviewerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reviewed");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("GraduationProject.Models.Case", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("GraduationProject.Models.Location.City", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("GraduationProject.Models.Location.Governorate", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("GraduationProject.Models.Mediator", b =>
                {
                    b.Navigation("CasesAdded");

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
