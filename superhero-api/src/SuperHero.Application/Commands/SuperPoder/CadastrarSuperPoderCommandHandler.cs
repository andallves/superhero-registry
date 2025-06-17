using AutoMapper;
using MediatR;
using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;

namespace SuperHero.Application.Commands.SuperPoder;

public class CadastrarSuperPoderCommandHandler : IRequestHandler<CadastrarSuperPoderCommand, CustomResult<SuperPoderDto>>
{
    private readonly IRepository<SuperHeroDbContext> _repository;
    private readonly IMapper _mapper;

    public CadastrarSuperPoderCommandHandler(IMapper mapper, IRepository<SuperHeroDbContext> repository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomResult<SuperPoderDto>> Handle(CadastrarSuperPoderCommand request, CancellationToken cancellationToken)
    {
    
        var superPoder = _mapper.Map<Domain.Entities.Hero.SuperPoder>(request);
        
        _repository.DbSet<Domain.Entities.Hero.SuperPoder>().Add(superPoder);
    
        return await _repository.SaveChangesAsync(cancellationToken) > 0
            ? CustomResult<SuperPoderDto>.SuccessResult(_mapper.Map<SuperPoderDto>(superPoder), "SuperPoder cadastrado com sucesso!")
            : CustomResult<SuperPoderDto>.ErrorResult("Erro ao criar o super poder.", errorType: EResultErrorType.ServerError);  
    }
}