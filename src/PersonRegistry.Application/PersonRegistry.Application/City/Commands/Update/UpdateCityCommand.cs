using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.City.Commands.Update;

/// <summary>
/// Command to update an existing city.
/// </summary>
 public record UpdateCityCommand(int Id, string Name) : IRequest<Unit>, ITransactionalRequest;