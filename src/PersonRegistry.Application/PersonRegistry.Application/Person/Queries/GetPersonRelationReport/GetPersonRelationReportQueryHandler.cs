using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Person.Queries.GetPersonRelationReport;

/// <summary>
/// Handles the retrieval of a report on person relations.
/// </summary>
/// <param name="_unitOfWork">Unit of work interface for accessing the repository layer.</param>
public class GetPersonRelationReportQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetPersonRelationReportQuery, List<PersonRelationReportModelResponse>>
{
    /// <summary>
    /// Handles the request to generate a report of person relations.
    /// </summary>
    /// <param name="request">The query request object.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A list of <see cref="PersonRelationReportModelResponse"/> containing relation report details.
    /// </returns>
    public async Task<List<PersonRelationReportModelResponse>> Handle(GetPersonRelationReportQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.PersonRelationRepository.GetAllRelations()
                        .GroupBy(pr => new
                        {
                            pName = pr.Person.Name,
                            pLastName = pr.Person.LastName,
                            pNumber = pr.Person.PersonalNumber,
                            prName = pr.PersonRelationType.Name
                        })
                        .Select(grouped => new PersonRelationReportModelResponse
                               (grouped.Key.pName,
                                grouped.Key.pLastName,
                                grouped.Key.pNumber,
                                grouped.Key.prName,
                                grouped.Count())
                               )
                        .ToListAsync();
    }
}