using AutoMapper;
using SuperHero.Application.Commands.SuperPoder;
using SuperHero.Application.DTO;
using SuperHero.Domain.Entities.Hero;
using SuperHero.Domain.ValueObjects;

namespace SuperHero.Application.AutoMapperProfiles;

public class SuperPoderProfile: Profile
{
    public SuperPoderProfile()
    {
        CreateMap<SuperPoderDto, SuperPoder>()
            .ReverseMap();

        CreateMap<PagedResult<SuperPoder>, PagedResult<SuperPoderDto>>()
            .ReverseMap();

        CreateMap<CadastrarSuperPoderCommand, SuperPoder>().ReverseMap();
    }
}