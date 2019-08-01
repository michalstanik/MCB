﻿// <auto-generated />
using System;
using MCB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MCB.Data.Migrations
{
    [DbContext(typeof(MCBContext))]
    partial class MCBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MCB.Data.Domain.Geo.Continent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Continent");
                });

            modelBuilder.Entity("MCB.Data.Domain.Geo.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alpha2Code");

                    b.Property<string>("Alpha3Code");

                    b.Property<long>("Area");

                    b.Property<string>("Name");

                    b.Property<string>("OfficialName");

                    b.Property<int?>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("MCB.Data.Domain.Geo.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContinentId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("MCB.Data.Domain.Trips.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("TripManagerId");

                    b.HasKey("Id");

                    b.HasIndex("TripManagerId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("MCB.Data.Domain.User.TUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("TUser");
                });

            modelBuilder.Entity("MCB.Data.Domain.User.UserCountry", b =>
                {
                    b.Property<int>("CountryId");

                    b.Property<string>("TUserId");

                    b.Property<long>("AreaLevelAssessment");

                    b.Property<string>("CountryKnowledgeType")
                        .IsRequired();

                    b.HasKey("CountryId", "TUserId");

                    b.HasIndex("TUserId");

                    b.ToTable("UserCountry");
                });

            modelBuilder.Entity("MCB.Data.Domain.User.UserTrip", b =>
                {
                    b.Property<int>("TripId");

                    b.Property<string>("TUserId");

                    b.HasKey("TripId", "TUserId");

                    b.HasIndex("TUserId");

                    b.ToTable("UserTrip");
                });

            modelBuilder.Entity("MCB.Data.Domain.Geo.Country", b =>
                {
                    b.HasOne("MCB.Data.Domain.Geo.Region", "Region")
                        .WithMany("Countries")
                        .HasForeignKey("RegionId");
                });

            modelBuilder.Entity("MCB.Data.Domain.Geo.Region", b =>
                {
                    b.HasOne("MCB.Data.Domain.Geo.Continent", "Continent")
                        .WithMany("Regions")
                        .HasForeignKey("ContinentId");
                });

            modelBuilder.Entity("MCB.Data.Domain.Trips.Trip", b =>
                {
                    b.HasOne("MCB.Data.Domain.User.TUser", "TripManager")
                        .WithMany()
                        .HasForeignKey("TripManagerId");
                });

            modelBuilder.Entity("MCB.Data.Domain.User.UserCountry", b =>
                {
                    b.HasOne("MCB.Data.Domain.Geo.Country", "Country")
                        .WithMany("UserCountries")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MCB.Data.Domain.User.TUser", "TUser")
                        .WithMany("UserCountries")
                        .HasForeignKey("TUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MCB.Data.Domain.User.UserTrip", b =>
                {
                    b.HasOne("MCB.Data.Domain.User.TUser", "TUser")
                        .WithMany("UserTrips")
                        .HasForeignKey("TUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MCB.Data.Domain.Trips.Trip", "Trip")
                        .WithMany("UserTrips")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
