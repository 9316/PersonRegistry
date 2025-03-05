using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.City.Commands.Delete;

/// <summary>
/// Handles the deletion of a city.
/// </summary>
public class DeleteCityCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteCityCommand, Unit>
{
    /// <summary>
    /// Handles the deletion of a city.
    /// </summary>
    /// <param name="request">The delete city command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An empty MediatR unit value.</returns>
    /// <exception cref="NotFoundException">Thrown when the city does not exist.</exception>
    public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _unitOfWork.CityRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound, nameof(Domain.Aggregates.City.City), request.Id));

        city.Delete();

        _unitOfWork.CityRepository.Update(city);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
