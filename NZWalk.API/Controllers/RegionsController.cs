using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;

namespace NZWalk.API.Controllers
{
    //https//localhost://portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dBContext;
        private readonly IRegionRepository regionRepository;
        public RegionsController(NZWalksDBContext dbContext, IRegionRepository regionRepository) {
            this.dBContext = dbContext;
            this.regionRepository = regionRepository;
        }

        //GET: http//localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database through data models
            //var regions = dBContext.Regions.ToList();
            //var regions = await dBContext.Regions.ToListAsync();
            var regions = await regionRepository.GetAllAsync();
            //Map the data models to dto
            var regionsDTO = new List<RegionDTO>();
            foreach(var region in regions)
            {
                regionsDTO.Add(new RegionDTO(){
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            };
                //Return the dto to the client.
;
            return Ok(regionsDTO);
        }
        //Get: http//localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //Get Region Domain model
            //var region = dBContext.Regions.Find(id);
            //var region = dbContext.REgions.FirstOrDefault(x=>x.Id == id);

            //Async
            //var region = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id
            //);

            var region = await regionRepository.GetAsync(id);

            //Map/Convert data model to DTO
            if(region == null)
            {
                return NotFound();
            }
            else
            {
                //Return DTO model to the client
                var regionsDTO = new RegionDTO
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                };
                return Ok(regionsDTO);
            }
        }
        //Post:http//localhost:portnumber/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //Map the DTO to domain model
            Region regionDomailModel = new Region
            {
                Name= addRegionRequestDTO.Name,
                Code= addRegionRequestDTO.Code,
                RegionImageUrl= addRegionRequestDTO.RegionImageUrl
            };
            //Create the region using data model
            //dBContext.Regions.Add(regionDomailModel);
            //dBContext.SaveChanges();

            //Async
            //await dBContext.Regions.AddAsync(regionDomailModel);
            //await dBContext.SaveChangesAsync();

            await regionRepository.CreateAsync(regionDomailModel);

            var regionsDto = new RegionDTO
            {
                Id = regionDomailModel.Id,
                Name = regionDomailModel.Name,
                Code = regionDomailModel.Code,
                RegionImageUrl = regionDomailModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionsDto.Id }, regionsDto);
        }

        //Update Region
        //PUT https//localhost:portnumber/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateRegionDTO updateRegionRequestDTO)
        {
            //Get the regionDomainModel using ID
            //var regionDomainModel = dBContext.Regions.FirstOrDefault(x => x.Id == id
            //);

            ////Async
            //var regionDomainModel = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = new Region
            {
                Id = id,
                Name = updateRegionRequestDTO.Name,
                Code = updateRegionRequestDTO.Code,
                RegionImageUrl = updateRegionRequestDTO.RegionImageUrl
            };
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            
            if(regionDomainModel!=null)
            {
                //Map DTO to domain model
                //regionDomainModel.Name = updateRegionRequestDTO.Name;
                //regionDomainModel.Code = updateRegionRequestDTO.Code;
                //regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

                //Save the changes in the database
                //dBContext.SaveChanges();

                //Async
                //await dBContext.SaveChangesAsync();

                //Convert domain model to DTO
                RegionDTO regionDTO = new RegionDTO
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                };
                return Ok(regionDTO);
            }
            else
            {
                //Return Not Found
                return NotFound();
            }
        }

        //Delete Region
        //DELETE https//localhost:portnumber//api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Get the regionDomainModel using the id
            //var regionDomainModel = dBContext.Regions.FirstOrDefault (x => x.Id == id);
            //var regionDomainModel = await dBContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);

            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if(regionDomainModel!=null) { 
                //dBContext.Regions.Remove(regionDomainModel);
                //dBContext.SaveChanges();

                //ASync NOTE - Remove method of dbContext does not have a Async dual
                //await dBContext.SaveChangesAsync();

                // Return DTO deleted item back
                RegionDTO regionDTO = new RegionDTO
                {
                    Id = regionDomainModel.Id,
                    Name = regionDomainModel.Name,
                    Code = regionDomainModel.Code,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                };
                return Ok(regionDTO);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
