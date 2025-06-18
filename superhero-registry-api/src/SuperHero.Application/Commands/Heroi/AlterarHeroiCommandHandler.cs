using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperHero.Application.DTO;
using SuperHero.Domain.Entities.Hero;
using SuperHero.Domain.ValueObjects;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;

namespace SuperHero.Application.Commands.Heroi;

public class AlterarHeroiCommandHandler: IRequestHandler<AlterarHeroiCommand, CustomResult<HeroiDto>>
{
    private readonly IRepository<SuperHeroDbContext> _repository;
    private readonly IMapper _mapper;

    public AlterarHeroiCommandHandler(IMapper mapper, IRepository<SuperHeroDbContext> repository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomResult<HeroiDto>> Handle(AlterarHeroiCommand request, CancellationToken cancellationToken)
    {
        var hero = await _repository.DbSet<Domain.Entities.Hero.Heroi>()
            .Include(h => h.HeroisSuperPoderes)
            .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

        if (hero is null)
        {
            return CustomResult<HeroiDto>.ErrorResult("Heroi não encontrado", errorType: EResultErrorType.NotFound);
        }

        _mapper.Map(request, hero);
        
        ManterHeroisSuperPoderes(ref hero, request.HeroisSuperPoderes);
        
        _repository.DbSet<Domain.Entities.Hero.Heroi>().Update(hero);
    
        return await _repository.SaveChangesAsync(cancellationToken) > 0
            ? CustomResult<HeroiDto>.SuccessResult(_mapper.Map<HeroiDto>(hero), "Alterações salva com sucesso!")
            : CustomResult<HeroiDto>.ErrorResult("Não foi possível atualizar o herói.", errorType: EResultErrorType.ServerError);  
    }
    
    private void ManterHeroisSuperPoderes(ref Domain.Entities.Hero.Heroi heroi, List<HeroiSuperPoderDto> poderesDto)
    {
        // Remover duplicados no DTO recebido
        var poderesUnicosDto = poderesDto
            .GroupBy(p => p.SuperPoderId)
            .Select(g => g.First())
            .ToList();

        // Remover poderes que não existem mais
        var poderesParaRemover = heroi.HeroisSuperPoderes
            .Where(p => poderesUnicosDto.All(dto => dto.SuperPoderId != p.SuperPoderId))
            .ToList();

        foreach (var poder in poderesParaRemover)
        {
            heroi.HeroisSuperPoderes.Remove(poder);
        }

        // Adicionar novos poderes que ainda não existem
        foreach (var dto in poderesUnicosDto)
        {
            bool jaExiste = heroi.HeroisSuperPoderes.Any(p => p.SuperPoderId == dto.SuperPoderId);

            if (!jaExiste)
            {
                var novoPoder = _mapper.Map<HeroiSuperPoder>(dto);
                heroi.HeroisSuperPoderes.Add(novoPoder);
            }
        }
    }
}