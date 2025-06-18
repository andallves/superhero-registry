using AutoMapper;
using SuperHero.Application.Commands.Heroi;
using SuperHero.Domain.Entities.Hero;

namespace SuperHero.Application.AutoMapperProfiles.resolvers;

public class HeroisSuperPoderesResolver : IValueResolver<AlterarHeroiCommand, Heroi, List<HeroiSuperPoder>>
{
    private readonly IMapper _mapper;
    
    public HeroisSuperPoderesResolver(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public List<HeroiSuperPoder> Resolve(AlterarHeroiCommand source, Heroi destination, List<HeroiSuperPoder> destMember, ResolutionContext context)
    {
        var heroisSuperPoderes = destMember.ToList();
        foreach (var heroiSuperPoder in source.HeroisSuperPoderes)
        {
            bool jaExiste = heroisSuperPoderes.Any(hsp =>
                hsp.SuperPoderId == heroiSuperPoder.SuperPoderId &&
                (heroiSuperPoder.Id == 0 || hsp.Id != 0));
            
            if (jaExiste)
                continue;
            
            if (heroiSuperPoder.Id is 0)
            {
                heroisSuperPoderes.Add( _mapper.Map<HeroiSuperPoder>(heroiSuperPoder));
                continue;
            }

            var existente = heroisSuperPoderes.FirstOrDefault(c => c!.Id == heroiSuperPoder.Id, null);
            if (existente == null) 
                continue;
            
            _mapper.Map(heroiSuperPoder, existente);
        }

        return heroisSuperPoderes;
    }
}