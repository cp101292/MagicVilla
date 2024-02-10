using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        // Note : These basic functionality is being provided  by [ApiController] which is used in Controller class as an attribute,
        // If we remove [ApiController] Attribute then these validation will not work as expected.
        // If we want this validation to work without the [ApiController] Attribute, then ModelState can be used in the respective action class.
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rates { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
