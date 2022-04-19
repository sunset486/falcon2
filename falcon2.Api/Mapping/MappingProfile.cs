using AutoMapper;
using falcon2.Api.Resources;
using falcon2.Core.Models;
using falcon2.Core.Models.Auth;
namespace falcon2.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Date la resurse
            CreateMap<SuperHero, SuperHeroResource>();
            CreateMap<SuperPower, SuperPowerResource>();

            //Resursa la date
            CreateMap<SuperHeroResource, SuperHero>();
            CreateMap<SuperPowerResource, SuperPower>();

            //Salvare valori noi
            CreateMap<SaveSuperHeroResource, SuperHero>();
            CreateMap<SaveSuperPowerResource, SuperPower>();

            //Creare utilizator nou (sign up)
            CreateMap<UserSignUpResource, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }
}
