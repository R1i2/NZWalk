using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dBContext;
        public SQLRegionRepository(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        async Task<List<Region>> IRegionRepository.GetAllAsync()
        {
            return await dBContext.Regions.ToListAsync();
        }
        async Task<Region?> IRegionRepository.GetAsync(System.Guid id)
        {
            return await dBContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
        }

        async Task<Region?> IRegionRepository.CreateAsync(NZWalk.API.Models.Domain.Region regionDomainModel)
        {
            await dBContext.Regions.AddAsync(regionDomainModel);
            await dBContext.SaveChangesAsync();
            return regionDomainModel;
        }

        async Task<Region?> IRegionRepository.UpdateAsync(Guid id,Region regionModel)
        {
            var region = await dBContext.Regions.FirstOrDefaultAsync(x=>x.Id== id);
            if(region!=null)
            {
                region.Name = regionModel.Name;
                region.Code = regionModel.Code;
                region.RegionImageUrl = regionModel.RegionImageUrl;
                await dBContext.SaveChangesAsync();
            }
            return region;
        }

        async public Task<Region?> DeleteAsync(Guid id)
        {
            var regionDomainModel = dBContext.Regions.FirstOrDefault(x=>x.Id== id);
            if(regionDomainModel != null)
            {
                dBContext.Regions.Remove(regionDomainModel);
                await dBContext.SaveChangesAsync();
            }
            return regionDomainModel;
        }
    }
}
