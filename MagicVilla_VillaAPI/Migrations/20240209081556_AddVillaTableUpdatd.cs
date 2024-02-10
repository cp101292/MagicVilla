using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaTableUpdatd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rates",
                table: "Villas",
                newName: "Rate");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[] { "", new DateTime(2024, 2, 9, 13, 45, 56, 701, DateTimeKind.Local).AddTicks(5927), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg", "Royal Villa", 4, 200.0, 550 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[] { "", new DateTime(2024, 2, 9, 13, 45, 56, 701, DateTimeKind.Local).AddTicks(5947), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg", "Premium Pool Villa", 4, 300.0, 550 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[] { "", new DateTime(2024, 2, 9, 13, 45, 56, 701, DateTimeKind.Local).AddTicks(5948), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg", "Luxury Pool Villa", 4, 400.0, 750 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[] { "", new DateTime(2024, 2, 9, 13, 45, 56, 701, DateTimeKind.Local).AddTicks(5950), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg", "Diamond Villa", 4, 550.0, 900 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[] { "", new DateTime(2024, 2, 9, 13, 45, 56, 701, DateTimeKind.Local).AddTicks(5951), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg", "Diamond Pool Villa", 4, 600.0, 1100 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Villas",
                newName: "Rates");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rates", "Sqft" },
                values: new object[] { "All", new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4765), "It faces the magic forest", "test1", "Royal villa", 10, 100000.0, 2000 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rates", "Sqft" },
                values: new object[] { "All", new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4780), "It faces the magic forest", "test2", "Lake view villa", 8, 200000.0, 3000 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rates", "Sqft" },
                values: new object[] { "All", new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4782), "Luxury villas", "test3", "Mantri vikash villa", 12, 900000.0, 5000 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rates", "Sqft" },
                values: new object[] { "All", new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4784), "It faces the beach", "test3", "Beach front villa", 12, 900000.0, 5000 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rates", "Sqft" },
                values: new object[] { "All", new DateTime(2024, 1, 28, 19, 4, 14, 47, DateTimeKind.Local).AddTicks(4787), "Beautiful forest villa forest", "test3", "Forest villa", 12, 900000.0, 5000 });
        }
    }
}
