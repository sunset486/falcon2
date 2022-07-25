using Microsoft.EntityFrameworkCore;
using falcon2.Core.Models;
using falcon2.Core.Repositories;

namespace falcon2.Data.Repositories
{
    public class SuperHeroRepository : Repository<SuperHero>, ISuperHeroRepository
    {
        private SuperDbContext SuperDbContext
        {
            get { return context as SuperDbContext; }
        }
        public SuperHeroRepository(SuperDbContext context)
            :base(context) 
        { }

        public async Task<IEnumerable<SuperHero>> GetAllWithSuperPowerAsync()
        {
            return await SuperDbContext.SuperHeroes
                .Include(sh => sh.SuperPowers)
                .ToListAsync();
        }

        public Task<SuperHero> GetWithSuperPowerByIdAsync(int id)
        {
            return SuperDbContext.SuperHeroes
                .Include(sh => sh.SuperPowers)
                .SingleOrDefaultAsync(sh => sh.Id == id);
        }
    }
}
