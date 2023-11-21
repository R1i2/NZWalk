using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Controllers
{
    //https//localhost://portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        public NZWalksDBContext dBContext { get; set; }
        public RegionsController(NZWalksDBContext dbContext) {
            this.dBContext = dbContext;
        }

        //GET: http//localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get data from database through data models
            var regions = dBContext.Regions.ToList();
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
        public IActionResult GetById(Guid id)
        {
            //Get Region Domain model
            //var region = dBContext.Regions.Find(id);
            var region = dBContext.Regions.FirstOrDefault(x => x.Id == id
            );
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
    }
}
