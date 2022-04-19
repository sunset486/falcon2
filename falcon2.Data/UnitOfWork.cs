using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falcon2.Core;
using falcon2.Core.Repositories;
using falcon2.Data.Repositories;

namespace falcon2.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SuperDbContext _context;
        private SuperHeroRepository _heroRepository;
        private SuperPowerRepository _powerRepository;

        public UnitOfWork(SuperDbContext context)
        {
            this._context = context;
        }

        public ISuperHeroRepository SuperHeroes => _heroRepository = _heroRepository ?? new SuperHeroRepository(_context);

        public ISuperPowerRepository SuperPowers => _powerRepository = _powerRepository ?? new SuperPowerRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
