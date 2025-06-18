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

    public List<HeroiSuperPoder> Resolve(AlterarHeroiCommand source, Heroi destination,
        List<HeroiSuperPoder> destMember, ResolutionContext context)
    {
        var heroisSuperPoderes = destMember.ToList();

        // Remove possíveis duplicações no próprio comando recebido
        var poderesUnicos = source.HeroisSuperPoderes
            .GroupBy(p => p.SuperPoderId)
            .Select(g => g.First());

        foreach (var heroiSuperPoder in poderesUnicos)
        {
            // Já existe na lista? Ignora!
            bool jaExiste = heroisSuperPoderes.Any(hsp => hsp.SuperPoderId == heroiSuperPoder.SuperPoderId);

            if (jaExiste)
                continue;

            if (heroiSuperPoder.Id == 0)
            {
                // Novo poder: adiciona mapeado
                heroisSuperPoderes.Add(_mapper.Map<HeroiSuperPoder>(heroiSuperPoder));
            }
            else
            {
                // Poder existente: atualiza se encontrado
                var existente = heroisSuperPoderes.FirstOrDefault(c => c.Id == heroiSuperPoder.Id);
                if (existente != null)
                    _mapper.Map(heroiSuperPoder, existente);
            }
        }

        return heroisSuperPoderes;
    }
}