using System.Diagnostics.CodeAnalysis;
using MediatR;
using SuperHero.Domain.ValueObjects;

namespace SuperHero.Application.Commands;

[ExcludeFromCodeCoverage]
public abstract class BaseCommand<T> : IRequest<CustomResult<T>>
{ }