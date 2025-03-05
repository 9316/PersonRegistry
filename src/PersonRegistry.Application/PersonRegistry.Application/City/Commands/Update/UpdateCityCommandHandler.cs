using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.City.Commands.Update;

/// <summary>
/// Handles the update of a city.
/// </summary>
public class UpdateCityCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateCityCommand, Unit>
{
    /// <summary>
    /// Handles the update of a city.
    /// </summary>
    /// <param name="request">The update city command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An empty MediatR unit value.</returns>
    /// <exception cref="AlreadyExistsException">Thrown if a city with the same name already exists.</exception>
    /// <exception cref="NotFoundException">Thrown when the city does not exist.</exception>
    public async Task<Unit> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CityRepository.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()))
            throw new AlreadyExistsException(string.Format(ExceptionMessageResource.RecordAlreadyExists, request.Name));

        var city = await _unitOfWork.CityRepository.GetByIdAsync(request.Id) ?? 
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound, nameof(Domain.Aggregates.City.City), request.Id));

        city.Update(request.Name);

        _unitOfWork.CityRepository.Update(city);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}