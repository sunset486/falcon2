using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using falcon2.Core.Services;
using falcon2.Core.Models;
using falcon2.Api.Resources;
using falcon2.Api.Validators;

namespace falcon2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;
        private readonly IMapper _mapper;
        public SuperHeroController(ISuperHeroService superHeroService, IMapper mapper)
        {
            this._mapper = mapper;
            this._superHeroService = superHeroService;
        }


        [HttpGet("GetAllSuperHeroes")]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetAllSuperHeroes()
        {
            var superHeroes = await _superHeroService.GetAllSuperHeroes();
            var superHeroResources = _mapper.Map<IEnumerable<SuperHero>, IEnumerable<SuperHeroResource>>(superHeroes);
            return Ok(superHeroResources);
        }

        [HttpGet("GetOneSuperHero")]
        public async Task<ActionResult<SuperHero>> GetOneSuperHero(int heroId)
        {
            var superHero = await _superHeroService.GetSuperHeroById(heroId);
            var superHeroResource = _mapper.Map<SuperHero, SuperHeroResource>(superHero);
            return Ok(superHeroResource);
        }

        [HttpPost("AddSuperHero")]
        public async Task<ActionResult<SuperHeroResource>> AddSuperHero([FromBody] SaveSuperHeroResource saveSuperHeroResource)
        {
            var createHero = _mapper.Map<SaveSuperHeroResource, SuperHero>(saveSuperHeroResource);
            var newHero = await _superHeroService.CreateSuperHero(createHero);
            var hero = await _superHeroService.GetSuperHeroById(newHero.Id);
            var resource = _mapper.Map<SuperHero, SuperHeroResource>(hero);

            return Ok(resource);

        }

        [HttpPut("UpdateSuperHero{id}")]
        public async Task<ActionResult<SuperHeroResource>> UpdateSuperHero(int id, [FromBody] SaveSuperHeroResource saveSuperHeroResource)
        {
            var heroToUpdate = await _superHeroService.GetSuperHeroById(id);

            if (heroToUpdate == null)
                return NotFound();

            var hero = _mapper.Map<SaveSuperHeroResource, SuperHero>(saveSuperHeroResource);

            await _superHeroService.UpdateSuperHero(heroToUpdate, hero);

            var updatedHero = await _superHeroService.GetSuperHeroById(id);
            var resource = _mapper.Map<SuperHero, SuperHeroResource>(updatedHero);

            return Ok(resource);
        }

        [HttpDelete("DeleteSuperHero{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            var hero = await _superHeroService.GetSuperHeroById(id);
            await _superHeroService.DeleteSuperHero(hero);
            
            return Ok("Super hero deleted!");
        }

    }
}
