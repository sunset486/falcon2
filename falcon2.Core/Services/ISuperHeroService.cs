using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falcon2.Core.Models;

namespace falcon2.Core.Services
{
    public interface ISuperHeroService
    {
        Task<IEnumerable<SuperHero>> GetAllSuperHeroes();
        Task<SuperHero> GetSuperHeroById(int id);
        Task<SuperHero> CreateSuperHero(SuperHero newHero);
        Task UpdateSuperHero(SuperHero oldHero, SuperHero hero);
        Task DeleteSuperHero(SuperHero hero);
    }
}
