using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonRegistry.API.Mappers;
using PersonRegistry.Application.PersonRelation.Command.Create;
using PersonRegistry.Application.PersonRelation.Command.Delete;

namespace PersonRegistry.API.Controllers;

/// <summary>
/// Controller for PersonRelations
/// </summary>
/// <param name="_mediator"></param>
[Route("api/[controller]")]
[ApiController]
public class PersonRelationController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Creates new relation of person
    /// </summary>
    /// <param name="request.name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost(Name = nameof(CreatePersonRelation))]
    public async Task<int> CreatePersonRelation([FromBody] CreatePersonRelationModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToCreatePersonRelationCommand(), cancellationToken);

    /// <summary>
    /// Deletes person relation by personId and relationId
    /// </summary>
    /// <param name="request.Id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(nameof(DeletePerson))]
    public async Task DeletePerson([FromBody] DeletePersonRelationModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToDeletePersonRelationCommand(), cancellationToken);
}
