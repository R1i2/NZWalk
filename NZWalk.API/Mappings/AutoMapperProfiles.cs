using AutoMapper;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region,AddRegionRequestDTO>().ReverseMap();
            CreateMap<Region,UpdateRegionDTO>().ReverseMap();
        }
    }
}
