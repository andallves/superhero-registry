using AutoMapper;
using MediatR;
using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;

namespace SuperHero.Application.Commands.Heroi;

public class CadastrarHeroiCommandHandler : IRequestHandler<CadastrarHeroiCommand, CustomResult<HeroiDto>>
{
    private readonly IRepository<SuperHeroDbContext> _repository;
    private readonly IMapper _mapper;

    public CadastrarHeroiCommandHandler(IMapper mapper, IRepository<SuperHeroDbContext> repository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomResult<HeroiDto>> Handle(CadastrarHeroiCommand request, CancellationToken cancellationToken)
    {
    
        var hero = _mapper.Map<Domain.Entities.Hero.Heroi>(request);
        
        _repository.DbSet<Domain.Entities.Hero.Heroi>().Add(hero);
    
        return await _repository.SaveChangesAsync(cancellationToken) > 0
            ? CustomResult<HeroiDto>.SuccessResult(_mapper.Map<HeroiDto>(hero), "Heroi cadastrado com sucesso!")
            : CustomResult<HeroiDto>.ErrorResult("Erro ao criar o curso.", errorType: EResultErrorType.ServerError);  
    }
}