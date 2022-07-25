using falcon2.Core;
using falcon2.Core.Models;
using falcon2.Core.Services;

namespace falcon2.Services
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SuperHeroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuperHero> CreateSuperHero(SuperHero newHero)
        {
            await _unitOfWork.SuperHeroes
                .AddAsync(newHero);
            await _unitOfWork.CommitAsync();
            return newHero;
        }

        public async Task DeleteSuperHero(SuperHero hero)
        {
            _unitOfWork.SuperHeroes.Remove(hero);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SuperHero>> GetAllSuperHeroes()
        {
            return await _unitOfWork.SuperHeroes.GetAllAsync();
        }

        public async Task<SuperHero> GetSuperHeroById(int id)
        {
            return await _unitOfWork.SuperHeroes.GetByIdAsync(id);
        }

        public async Task UpdateSuperHero(SuperHero oldHero, SuperHero hero)
        {
            oldHero.Name = hero.Name;
            await _unitOfWork.CommitAsync();
        }
    }
}
