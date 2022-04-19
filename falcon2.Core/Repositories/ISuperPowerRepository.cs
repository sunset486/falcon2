using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falcon2.Core.Models;

namespace falcon2.Core.Repositories
{
    public interface ISuperPowerRepository : IRepository<SuperPower>
    {
        Task<IEnumerable<SuperPower>> GetAllWithSuperHeroAsync();
        Task<SuperPower> GetWithSuperHeroByIdAsync(int id);
        Task<IEnumerable<SuperPower>> GetAllWithSuperHeroBySuperHeroIdAsync(int heroId);

    }
}
