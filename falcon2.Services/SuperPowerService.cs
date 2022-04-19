using falcon2.Core;
using falcon2.Core.Models;
using falcon2.Core.Services;

namespace falcon2.Services
{
    public class SuperPowerService : ISuperPowerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SuperPowerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuperPower> CreateSuperPower(SuperPower newPower)
        {
            await _unitOfWork.SuperPowers
                .AddAsync(newPower);
            await _unitOfWork.CommitAsync(); 
            return newPower;
        }

        public async Task DeleteSuperPower(SuperPower hero)
        {
            _unitOfWork.SuperPowers.Remove(hero);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SuperPower>> GetAllWithSuperHero()
        {
            return await _unitOfWork.SuperPowers.GetAllWithSuperHeroAsync();
        }

        public async Task<SuperPower> GetSuperPowerById(int id)
        {
            return await _unitOfWork.SuperPowers.GetWithSuperHeroByIdAsync(id);
        }

        public async Task <IEnumerable<SuperPower>> GetSuperPowersBySuperHeroId(int heroId)
        {
            return await _unitOfWork.SuperPowers
                .GetAllWithSuperHeroBySuperHeroIdAsync(heroId);
        }

        public async Task UpdateSuperPower(SuperPower oldPower, SuperPower power)
        {
            oldPower.Name = power.Name;
            oldPower.SuperHeroId = power.SuperHeroId;    
            await _unitOfWork.CommitAsync();
        }

        Task<IEnumerable<SuperPower>> ISuperPowerService.GetSuperPowersBySuperHeroId(int heroId)
        {
            throw new NotImplementedException();
        }
    }
}
