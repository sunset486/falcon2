using falcon2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falcon2.Core.Repositories
{
    public interface ISuperHeroRepository : IRepository<SuperHero>
    {
        Task <IEnumerable<SuperHero>> GetAllWithSuperPowerAsync();
        Task<SuperHero> GetWithSuperPowerByIdAsync(int id);

    }
}
