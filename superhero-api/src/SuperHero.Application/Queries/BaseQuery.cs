using MediatR;
using System.Diagnostics.CodeAnalysis;
using SuperHero.Domain.ValueObjects;

namespace SuperHero.Application.Queries;

[ExcludeFromCodeCoverage]
public abstract class BaseQuery<TSearchEntity> : IRequest<CustomResult<TSearchEntity>>
{ }