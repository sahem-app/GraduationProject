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
    [Migration("20220318151241_CreateTriggersToCascadeDeleteGeoLocationsTable")]
    partial class CreateTriggersToCascadeDeleteGeoLocationsTable
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

                    b.Property<DateTime?>("DateLimit")
                        .HasColumnType("date");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("date");

                    b.Property<byte>("GenderId")
                        .HasColumnType("tinyint");

                    b.Property<int>("GeoLocationId")
                        .HasColumnType("int");

                    b.Property<int>("MediatorId")
                        .HasColumnType("int");

                    b.Property<int?>("MediatorId1")
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

                    b.HasIndex("MediatorId1");

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

            modelBuilder.Entity("GraduationProject.Models.City", b =>
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

            modelBuilder.Entity("GraduationProject.Models.Gender", b =>
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

            modelBuilder.Entity("GraduationProject.Models.GeoLocation", b =>
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

            modelBuilder.Entity("GraduationProject.Models.Governorate", b =>
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

                    b.Property<byte>("GenderId")
                        .HasColumnType("tinyint");

                    b.Property<int>("GeoLocationId")
                        .HasColumnType("int");

                    b.Property<string>("Job")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

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

                    b.HasAlternateKey("NationalId");

                    b.HasAlternateKey("PhoneNumber");

                    b.HasIndex("GenderId");

                    b.HasIndex("GeoLocationId")
                        .IsUnique();

                    b.HasIndex("RegionId");

                    b.HasIndex("SocialStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("Mediators");
                });

            modelBuilder.Entity("GraduationProject.Models.Region", b =>
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

            modelBuilder.Entity("GraduationProject.Models.SocialStatus", b =>
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

            modelBuilder.Entity("GraduationProject.Models.Status", b =>
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
                        .WithMany("Cases")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.GeoLocation", "GeoLocation")
                        .WithOne()
                        .HasForeignKey("GraduationProject.Models.Case", "GeoLocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Mediator", "Mediator")
                        .WithMany()
                        .HasForeignKey("MediatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Mediator", null)
                        .WithMany("CasesAdded")
                        .HasForeignKey("MediatorId1");

                    b.HasOne("GraduationProject.Models.CaseProperties.Priority", "Priority")
                        .WithMany("Cases")
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.CaseProperties.Relationship", "Relationship")
                        .WithMany()
                        .HasForeignKey("RelationshipId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.SocialStatus", "SocialStatus")
                        .WithMany()
                        .HasForeignKey("SocialStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Gender");

                    b.Navigation("GeoLocation");

                    b.Navigation("Mediator");

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

            modelBuilder.Entity("GraduationProject.Models.City", b =>
                {
                    b.HasOne("GraduationProject.Models.Governorate", "Governorate")
                        .WithMany("Cities")
                        .HasForeignKey("GovernorateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("GraduationProject.Models.Mediator", b =>
                {
                    b.HasOne("GraduationProject.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.GeoLocation", "GeoLocation")
                        .WithOne()
                        .HasForeignKey("GraduationProject.Models.Mediator", "GeoLocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GraduationProject.Models.SocialStatus", "SocialStatus")
                        .WithMany()
                        .HasForeignKey("SocialStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("GeoLocation");

                    b.Navigation("Region");

                    b.Navigation("SocialStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("GraduationProject.Models.Region", b =>
                {
                    b.HasOne("GraduationProject.Models.City", "City")
                        .WithMany("Regions")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("GraduationProject.Models.Case", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("GraduationProject.Models.CaseProperties.Category", b =>
                {
                    b.Navigation("Cases");
                });

            modelBuilder.Entity("GraduationProject.Models.CaseProperties.Priority", b =>
                {
                    b.Navigation("Cases");
                });

            modelBuilder.Entity("GraduationProject.Models.City", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("GraduationProject.Models.Governorate", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("GraduationProject.Models.Mediator", b =>
                {
                    b.Navigation("CasesAdded");
                });
#pragma warning restore 612, 618
        }
    }
}
