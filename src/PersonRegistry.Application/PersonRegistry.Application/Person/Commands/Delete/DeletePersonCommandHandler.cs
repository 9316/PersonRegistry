using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Person.Commands.Delete;

/// <summary>
/// Handles the deletion of a person.
/// </summary>
public class DeletePersonCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeletePersonCommand, Unit>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work for managing transactions.</param>
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.PersonRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound, nameof(Domain.Aggregates.Person.Person), request.Id));

        person.Delete();

        _unitOfWork.PersonRepository.Update(person);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}