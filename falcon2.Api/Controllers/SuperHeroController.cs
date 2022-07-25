using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System.Text;
using AutoMapper;
using falcon2.Core.Services;
using falcon2.Core.Models;
using falcon2.Api.Resources;
using falcon2.Api.Helpers;

namespace falcon2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;
        private readonly IMapper _mapper;
        private readonly IReflectionInfoService _reflectionInfoService;
        private readonly ISpreadsheetService<SuperHeroResource> _spreadsheetService;
        private readonly IExcelImportService _excelImportService;
        public SuperHeroController(ISuperHeroService superHeroService, IMapper mapper, 
            IReflectionInfoService reflectionInfoService, 
            ISpreadsheetService<SuperHeroResource> spreadsheetService,
            IExcelImportService excelImportService)
        {
            _mapper = mapper;
            _superHeroService = superHeroService;
            _reflectionInfoService = reflectionInfoService;
            _spreadsheetService = spreadsheetService;
            _excelImportService = excelImportService;
        }

        [HttpGet("ReflectionForTypes")]
        public async Task<IActionResult> GetSuperHeroTypes()
        {
            Console.WriteLine("\n\t\t\t---ALL INFORMATION FOR SuperHero---\n\n\n");
            _reflectionInfoService.GetStaticInfo(typeof(SuperHero));
            _reflectionInfoService.GetInstanceInfo(typeof(SuperHero));
            Console.WriteLine("\t\t\t---ALL INFORMATION FOR SuperHeroResource---\n\n\n");
            _reflectionInfoService.GetStaticInfo(typeof(SuperHeroResource));
            _reflectionInfoService.GetInstanceInfo(typeof(SuperHeroResource));
            return Ok("Info returned for the controller's types");
        }

        [HttpGet("GetAllSuperHeroes")]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetAllSuperHeroes()
        {
            var superHeroes = await _superHeroService.GetAllSuperHeroes();
            var superHeroResources = _mapper.Map<IEnumerable<SuperHero>, IEnumerable<SuperHeroResource>>(superHeroes);
            //CreateNewSpreadSheet(superHeroResources);
            return Ok(superHeroResources);
        }

        [HttpGet("GetOneSuperHero")]
        public async Task<ActionResult<SuperHero>> GetOneSuperHero(int heroId)
        {
            var superHero = await _superHeroService.GetSuperHeroById(heroId);
            var superHeroResource = _mapper.Map<SuperHero, SuperHeroResource>(superHero);
            return Ok(superHeroResource);
        }

        [HttpPost("GenerateSuperHeroSpreadsheet")]
        public async Task<ActionResult<IEnumerable<SuperHero>>> CreateSuperHeroSpreadsheet()
        {
            var superHeroes = await _superHeroService.GetAllSuperHeroes();
            var superHeroResources = _mapper.Map<IEnumerable<SuperHero>, IEnumerable<SuperHeroResource>>(superHeroes);
            await _spreadsheetService.GenerateSpreadsheet(superHeroResources, "SuperHeroes");
            return Ok("Spreadsheet created");
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

        [HttpPost("AddSuperHeroFromSpreadsheet")]
        public async Task<ActionResult> AddSuperHeroFromSpreadsheet(IFormFile spreadsheet, string column, string value)
        {
            await _excelImportService.InsertHeroFromExcel(spreadsheet, column, value);
            return Ok("Data succesfully inserted!");
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
