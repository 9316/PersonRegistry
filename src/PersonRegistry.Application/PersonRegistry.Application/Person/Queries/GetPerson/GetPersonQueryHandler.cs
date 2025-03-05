using MediatR;
using PersonRegistry.Application.Common.Mappers;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Person.Queries.GetPerson;

/// <summary>
/// Handles the retrieval of a person's details by their ID.
/// </summary>
/// <param name="_unitOfWork">Unit of work interface for accessing the repository layer.</param>
public class GetPersonQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetPersonQuery, GetPersonModelResponse>
{
    /// <summary>
    /// Handles the request to retrieve a person's details by ID.
    /// </summary>
    /// <param name="request">The query containing the ID of the person to retrieve.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="GetPersonModelResponse"/> containing the person's details.</returns>
    /// <exception cref="NotFoundException">
    /// Thrown if a person with the specified ID is not found.
    /// </exception>
    public async Task<GetPersonModelResponse> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.PersonRepository.GetDetailsByIdAsync(request.Id) ??
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                        nameof(Domain.Aggregates.Person.Person), request.Id));

        return person.ToPersonResponse();
    }
}