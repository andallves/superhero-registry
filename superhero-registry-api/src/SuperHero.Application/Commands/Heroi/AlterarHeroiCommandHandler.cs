using AutoMapper;
using MediatR;
using SuperHero.Application.DTO;
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
    
        var hero = _mapper.Map<Domain.Entities.Hero.Heroi>(request);
        
        _repository.DbSet<Domain.Entities.Hero.Heroi>().Update(hero);
    
        return await _repository.SaveChangesAsync(cancellationToken) > 0
            ? CustomResult<HeroiDto>.SuccessResult(_mapper.Map<HeroiDto>(hero), "Alterações salva com sucesso!")
            : CustomResult<HeroiDto>.ErrorResult("Erro ao atualizar o herói.", errorType: EResultErrorType.ServerError);  
    }
}