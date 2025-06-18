using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;

namespace SuperHero.Application.Queries.Heroi;

public class ObterHeroiPorIdQueryHandler: IRequestHandler<ObterHeroiPorIdQuery, CustomResult<HeroiDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<SuperHeroDbContext> _repository;
    
    public ObterHeroiPorIdQueryHandler(IMapper mapper, IRepository<SuperHeroDbContext> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CustomResult<HeroiDto>> Handle(ObterHeroiPorIdQuery request, CancellationToken cancellationToken)
    {
        var heroi = await _repository
            .GetQueryable<Domain.Entities.Hero.Heroi>()
            .Include(h => h.HeroisSuperPoderes)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.Desativado, cancellationToken);
        
        return heroi == null
            ? CustomResult<HeroiDto>.ErrorResult("Heroi não encontrado", errorType: EResultErrorType.NotFound)
            : CustomResult<HeroiDto>.SuccessResult(_mapper.Map<HeroiDto>(heroi));    
    }
}