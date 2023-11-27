using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Repositories
{
    public class InMemoryRepsoitory : IRegionRepository
    {
        async Task<List<Region>> IRegionRepository.GetAllAsync()
        {
            return new List<Region>
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "Some Random Region",
                    Name = "Random RAndom"
                }
            };
        }
        async Task<Region?> IRegionRepository.GetAsync(System.Guid id)
        {
            return new Region()
            {
                Id = Guid.NewGuid(),
                Code = "Some Random Region",
                Name = "Random RAndom"
            };
        }
        async Task<Region?> IRegionRepository.CreateAsync(NZWalk.API.Models.Domain.Region regionDomainModel)
        {
            return regionDomainModel;
        }

        async Task<Region> IRegionRepository.UpdateAsync(Guid id, Region regionModel)
        {
            return new Region
            {
                Id = id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                RegionImageUrl = regionModel.RegionImageUrl
            };
        }

        async Task<Region?> IRegionRepository.DeleteAsync(Guid id)
        {
            return new Region
            {
                Id = id,
                Name = "Test",
                Code = "Test",
                RegionImageUrl = "Test"
            };
        }
    }
}
