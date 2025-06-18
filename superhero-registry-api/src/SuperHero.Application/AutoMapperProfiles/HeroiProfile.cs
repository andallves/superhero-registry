using AutoMapper;
using SuperHero.Application.AutoMapperProfiles.resolvers;
using SuperHero.Application.Commands.Heroi;
using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;
using SuperHero.Domain.Entities.Hero;

namespace SuperHero.Application.AutoMapperProfiles;

public class HeroiProfile: Profile
{
    public HeroiProfile()
    {
        CreateMap<PagedResult<HeroiDto>, PagedResult<Heroi>>()
            .ReverseMap();
        CreateMap<HeroiDto, Heroi>()
            .ReverseMap();
        
        CreateMap<CadastrarHeroiCommand, Heroi>();
        CreateMap<AlterarHeroiCommand, Heroi>();
            // .ForMember(dest
            //     => dest.HeroisSuperPoderes, opt
            //     => opt.MapFrom<HeroisSuperPoderesResolver>());
    }
}

public class HeroiSuperPoderProfile : Profile
{
    public HeroiSuperPoderProfile()
    {
        CreateMap<HeroiSuperPoder, HeroiSuperPoderDto>()
            .ForMember(x => x.HeroiId, opt => opt.MapFrom(src => src.HeroiId))
            .ForMember(x => x.SuperPoderId, opt => opt.MapFrom(src => src.SuperPoderId));

        CreateMap<HeroiSuperPoderDto, HeroiSuperPoder>()
            .ForMember(x => x.HeroiId, opt => opt.MapFrom(src => src.HeroiId))
            .ForMember(x => x.SuperPoderId, opt => opt.MapFrom(src => src.SuperPoderId));
    }
}