using MagicVilla_VillaAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) 
            : base(option)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal villa",
                    Details = "It faces the magic forest",
                    Rates = 100000,
                    Sqft = 2000,
                    Occupancy = 10,
                    ImageUrl = "test1",
                    Amenity = "All",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Lake view villa",
                    Details = "It faces the magic forest",
                    Rates = 200000,
                    Sqft = 3000,
                    Occupancy = 8,
                    ImageUrl = "test2",
                    Amenity = "All",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 3,
                    Name = "Mantri vikash villa",
                    Details = "Luxury villas",
                    Rates = 900000,
                    Sqft = 5000,
                    Occupancy = 12,
                    ImageUrl = "test3",
                    Amenity = "All",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 4,
                    Name = "Beach front villa",
                    Details = "It faces the beach",
                    Rates = 900000,
                    Sqft = 5000,
                    Occupancy = 12,
                    ImageUrl = "test3",
                    Amenity = "All",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 5,
                    Name = "Forest villa",
                    Details = "Beautiful forest villa forest",
                    Rates = 900000,
                    Sqft = 5000,
                    Occupancy = 12,
                    ImageUrl = "test3",
                    Amenity = "All",
                    CreatedDate = DateTime.Now,
                }
            );
        }
    }
}
