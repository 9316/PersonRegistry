using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.City.Commands.Create;

/// <summary>
/// Handles the creation of a new city.
/// </summary>
public class CreateCityCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateCityCommand, int>
{

    /// <summary>
    /// Handles the creation of a new city.
    /// </summary>
    /// <param name="request">The create city command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ID of the created city.</returns>
    /// <exception cref="AlreadyExistsException">Thrown if a city with the same name already exists.</exception>
    public async Task<int> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.CityRepository.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()))
            throw new AlreadyExistsException(string.Format(ExceptionMessageResource.RecordAlreadyExists, request.Name));

        var city = Domain.Aggregates.City.City.Create(request.Name.Trim());

        await _unitOfWork.CityRepository.AddAsync(city, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return city.Id;
    }
}