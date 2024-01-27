using MagicVilla_VillaAPI.Model.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList =  new List<VillaDto>()
        {
            new () { Id = 1, Name = "Pool View" , Sqft=100, Occupancy=4},
            new () { Id = 2, Name = "Beach view", Sqft=300, Occupancy=3}
        };
    }
}
