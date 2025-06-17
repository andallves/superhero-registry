using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;

namespace SuperHero.Application.Commands.Heroi;

public class HabilitarHeroiCommandHandler : IRequestHandler<HabilitarHeroiCommand, CustomResult<HeroiDto>>
{
    private readonly IRepository<SuperHeroDbContext> _repository;
    private readonly IMapper _mapper;
    
    public HabilitarHeroiCommandHandler(IRepository<SuperHeroDbContext> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomResult<HeroiDto>> Handle(HabilitarHeroiCommand request, CancellationToken cancellationToken)
    {
        var heroi = await _repository.DbSet<Domain.Entities.Hero.Heroi>().FirstOrDefaultAsync(c => c.Id == request.HeroiId, cancellationToken);
        if (heroi is null)
        {
            return CustomResult<HeroiDto>.ErrorResult("Curso não encontrado", errorType: EResultErrorType.NotFound);
        }
        
        heroi.Habilitar();
        _repository.DbSet<Domain.Entities.Hero.Heroi>().Update(heroi);
        
        return await _repository.SaveChangesAsync(cancellationToken) > 0
            ? CustomResult<HeroiDto>.SuccessResult(_mapper.Map<HeroiDto>(heroi), "Curso habilitado com sucesso!")
            : CustomResult<HeroiDto>.ErrorResult("Erro ao habilitar o curso.", errorType: EResultErrorType.ServerError);
    }
}