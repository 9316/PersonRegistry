using MediatR;

namespace PersonRegistry.Application.Person.Queries.GetPersonRelationReport
{
    public class GetPersonRelationReportQuery : IRequest<List<PersonRelationReportModelResponse>>
    {
    }
}
