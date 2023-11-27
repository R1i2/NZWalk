using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;
using NZWalk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalksController(IWalkRepository walkRepository, IMapper mapper)    
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            //Map the DTO to domain model
            var walks = mapper.Map<Walk>(addWalkRequestDTO);

            walks = await walkRepository.CreateAsync(walks);
            WalkDTO walkDTO = mapper.Map<WalkDTO>(walks);
            return CreatedAtAction(nameof(GetById), new { id = walkDTO.Id }, walkDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult>GetById(Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk != null)
            {
                var walkDTO = mapper.Map<WalkDTO>(walk);
                return Ok(walkDTO);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAllAsync();

            var walkDTO = mapper.Map<List<WalkDTO>>(walks);
            return Ok(walkDTO);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDTO updateWalkDTO)
        {
            var walks = mapper.Map<Walk>(updateWalkDTO);
            walks = await walkRepository.UpdateAsync(id, walks);
            if (walks != null)
            {
                WalkDTO walkDTO = mapper.Map<WalkDTO>(walks);
                return Ok(walkDTO);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walks = await walkRepository.DeleteAsync(id);
            if(walks!= null)
            {
                WalkDTO walksDTO = mapper.Map<WalkDTO>(walks);
                return Ok(walksDTO);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
