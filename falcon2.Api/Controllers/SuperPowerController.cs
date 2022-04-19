using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using falcon2.Core.Services;
using falcon2.Core.Models;
using falcon2.Api.Resources;
using falcon2.Api.Validators;

namespace falcon2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperPowerController : ControllerBase
    {
        private readonly ISuperPowerService _superPowerService;
        private readonly IMapper _mapper;
        public SuperPowerController(ISuperPowerService superPowerService, IMapper mapper)
        {
            this._mapper = mapper;
            this._superPowerService = superPowerService;
        }


        [HttpGet("GetAllSuperPowers")]
        public async Task<ActionResult<IEnumerable<SuperPower>>> GetAllSuperPowers()
        {
            var superPowers = await _superPowerService.GetAllWithSuperHero();
            var superPowerResources = _mapper.Map<IEnumerable<SuperPower>, IEnumerable<SuperPowerResource>>(superPowers);
            return Ok(superPowerResources);
        }

        [HttpGet("GetOneSuperPower")]
        public async Task<ActionResult<SuperPower>> GetOneSuperPower(int powerId)
        {
            var superPowers = await _superPowerService.GetSuperPowerById(powerId);
            var superPowerResources = _mapper.Map<SuperPower, SuperPowerResource>(superPowers);
            return Ok(superPowerResources);
        }

        [HttpPost("AddSuperPower")]
        public async Task<ActionResult<SuperPowerResource>> AddSuperPower([FromBody] SaveSuperPowerResource saveSuperPowerResource)
        {
            var createPower = _mapper.Map<SaveSuperPowerResource, SuperPower> (saveSuperPowerResource);
            var newPower = await _superPowerService.CreateSuperPower(createPower);
            var power = await _superPowerService.GetSuperPowerById (newPower.Id);
            var resource = _mapper.Map<SuperPower, SuperPowerResource>(power);

            return Ok(resource);
        }

        [HttpPut("UpdateSuperPower{id}")]
        public async Task<ActionResult<SuperPowerResource>> UpdateSuperPower(int id, [FromBody] SaveSuperPowerResource saveSuperPowerResource)
        {
            var powerToUpdate = await _superPowerService.GetSuperPowerById(id);

            if (powerToUpdate == null)
                return NotFound();

            var power = _mapper.Map<SaveSuperPowerResource, SuperPower>(saveSuperPowerResource);

            await _superPowerService.UpdateSuperPower(powerToUpdate, power);

            var updatedPower = await _superPowerService.GetSuperPowerById(id);
            var resource = _mapper.Map<SuperPower, SuperPowerResource>(updatedPower);

            return Ok(resource);
        }

        [HttpDelete("DeleteSuperPower{id}")]
        public async Task<IActionResult> DeleteSuperPower(int id)
        {
            if (id == 0)
                return BadRequest("No data with id=0!");

            var power = await _superPowerService.GetSuperPowerById(id);

            if (power == null)
                return NotFound("Super power does not exist!");

            await _superPowerService.DeleteSuperPower(power);

            return Ok("Super power deleted!");
        }
    }
}
