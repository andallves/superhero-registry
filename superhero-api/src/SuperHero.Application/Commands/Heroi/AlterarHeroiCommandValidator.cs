using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;

namespace SuperHero.Application.Commands.Heroi;

public class AlterarHeroiCommandValidator : AbstractValidator<CadastrarHeroiCommand>
{
    private readonly IRepository<SuperHeroDbContext> _repository;

    public AlterarHeroiCommandValidator(IRepository<SuperHeroDbContext> repository)
    {
        _repository = repository;
        
        RuleFor(c => c.Nome)
            .NotEmpty()
            .NotNull()
            .MaximumLength(120);
        
        RuleFor(c => c.NomeHeroi)
            .NotEmpty()
            .NotNull()
            .MustAsync(NomeHeroiUnico).WithMessage("Já existe um Heroi com esse nome.")
            .MaximumLength(120);

        RuleFor(c => c.Altura)
            .NotEmpty()
            .NotNull();
        
        RuleFor(c => c.Peso)
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.HeroisSuperPoderes)
            .Must(x => x.Count > 0)
            .WithMessage("Deve enviar ao menos um superpoder.")
            .Must(x => x.Count == x.DistinctBy(i => i.Id).Count())
            .WithMessage("Não pode conter superpoderes repetidos.");
    }
    
    private async Task<bool> NomeHeroiUnico(string nome, CancellationToken cancellationToken)
    {
        var nomeHeroiExistente = await _repository.DbSet<Domain.Entities.Hero.Heroi>()
            .Where(c => !c.Desativado)
            .AnyAsync(c => c.Nome.ToUpper().Equals(nome.ToUpper()), cancellationToken);
    
        return !nomeHeroiExistente;
    }
}