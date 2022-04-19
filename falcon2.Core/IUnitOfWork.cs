using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falcon2.Core.Repositories;

namespace falcon2.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISuperHeroRepository SuperHeroes { get; }
        ISuperPowerRepository SuperPowers { get; }
        Task<int> CommitAsync();
    }
}
