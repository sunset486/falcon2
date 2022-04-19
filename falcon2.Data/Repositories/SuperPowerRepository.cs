using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using falcon2.Core.Models;
using falcon2.Core.Repositories;

namespace falcon2.Data.Repositories
{
    public class SuperPowerRepository : Repository<SuperPower>, ISuperPowerRepository
    {
        private SuperDbContext SuperDbContext
        {
            get { return context as SuperDbContext; }
        }

        public SuperPowerRepository(SuperDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<SuperPower>> GetAllWithSuperHeroAsync()
        {
            return await SuperDbContext.SuperPowers
                .Include(sp => sp.SuperHero)
                .ToListAsync();
        }

        public async Task<SuperPower> GetWithSuperHeroByIdAsync(int id)
        {
            return await SuperDbContext.SuperPowers
                .Include(sp=>sp.SuperHero)
                .SingleOrDefaultAsync(sp=>sp.Id == id);
        }

        public async Task<IEnumerable<SuperPower>> GetAllWithSuperHeroBySuperHeroIdAsync(int heroId)
        {
            return await SuperDbContext.SuperPowers
                .Include(sp => sp.SuperHero)
                .Where(sp => sp.Id == heroId)
                .ToListAsync();
        }

        
    }
}
