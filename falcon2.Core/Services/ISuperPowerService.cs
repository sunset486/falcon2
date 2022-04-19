using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falcon2.Core.Models;

namespace falcon2.Core.Services
{
    public interface ISuperPowerService
    {
        Task<IEnumerable<SuperPower>> GetAllWithSuperHero();
        Task<SuperPower> GetSuperPowerById(int id);
        Task<IEnumerable<SuperPower>> GetSuperPowersBySuperHeroId(int heroId);
        Task<SuperPower> CreateSuperPower(SuperPower superPower);
        Task UpdateSuperPower(SuperPower oldPower, SuperPower power);
        Task DeleteSuperPower(SuperPower power);
    }
}
