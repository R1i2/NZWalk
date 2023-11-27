using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);

        Task<Region> CreateAsync(Region regionDomainModel);

        Task<Region> UpdateAsync(Guid id,Region regionModel);

        Task<Region> DeleteAsync(Guid id);
    }
}
