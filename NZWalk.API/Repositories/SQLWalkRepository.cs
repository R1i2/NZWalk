using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext dbContext;
        public SQLWalkRepository(NZWalksDBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        async Task<List<Walk>> IWalkRepository.GetAllAsync()
        {
            return await dbContext.Walks.ToListAsync();
        }

        async Task<Walk?> IWalkRepository.CreateAsync(Walk walk)
        {
           await dbContext.Walks.AddAsync(walk);
           await dbContext.SaveChangesAsync();
           return walk;
        }

        async Task<Walk?> IWalkRepository.GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task<Walk?> IWalkRepository.UpdateAsync(Guid id, Walk walk)
        {
            var walksDomain = await dbContext.Walks.FirstOrDefaultAsync(x=>x.Id== id);
            if (walksDomain != null)
            {
                walksDomain.Description = walk.Description;
                walksDomain.Name = walk.Name;
                walksDomain.LengthInKm = walk.LengthInKm;
                walksDomain.WalkImageUrl = walk.WalkImageUrl;
                await dbContext.SaveChangesAsync();
            }
            return walksDomain;
        }

        async Task<Walk?> IWalkRepository.DeleteAsync(Guid id)
        {
            var walks = await dbContext.Walks.FirstOrDefaultAsync(x=> x.Id== id);
            if (walks != null)
            {
                dbContext.Walks.Remove(walks);
                await dbContext.SaveChangesAsync();
            }
            return walks;
        }
    }
}
