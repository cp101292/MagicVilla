﻿// <auto-generated />
using System;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240128133414_SeedVillaTableCreate")]
    partial class SeedVillaTableCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_VillaAPI.Model.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rates")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "All",
                            CreatedDate = new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4765),
                            Details = "It faces the magic forest",
                            ImageUrl = "test1",
                            Name = "Royal villa",
                            Occupancy = 10,
                            Rates = 100000.0,
                            Sqft = 2000,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "All",
                            CreatedDate = new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4780),
                            Details = "It faces the magic forest",
                            ImageUrl = "test2",
                            Name = "Lake view villa",
                            Occupancy = 8,
                            Rates = 200000.0,
                            Sqft = 3000,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "All",
                            CreatedDate = new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4782),
                            Details = "Luxury villas",
                            ImageUrl = "test3",
                            Name = "Mantri vikash villa",
                            Occupancy = 12,
                            Rates = 900000.0,
                            Sqft = 5000,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Amenity = "All",
                            CreatedDate = new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4784),
                            Details = "It faces the beach",
                            ImageUrl = "test3",
                            Name = "Beach front villa",
                            Occupancy = 12,
                            Rates = 900000.0,
                            Sqft = 5000,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Amenity = "All",
                            CreatedDate = new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4787),
                            Details = "Beautiful forest villa forest",
                            ImageUrl = "test3",
                            Name = "Forest villa",
                            Occupancy = 12,
                            Rates = 900000.0,
                            Sqft = 5000,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
