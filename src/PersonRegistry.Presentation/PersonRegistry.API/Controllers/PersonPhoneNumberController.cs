using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonRegistry.API.Mappers;
using PersonRegistry.Application.PersonPhoneNumber.Command.Create;
using PersonRegistry.Application.PersonPhoneNumber.Command.Delete;
using PersonRegistry.Application.PersonPhoneNumber.Command.Update;

namespace PersonRegistry.API.Controllers;

/// <summary>
/// Controller for PersonPhoneNumbers
/// </summary>
/// <param name="_mediator"></param>
[Route("api/[controller]")]
[ApiController]
public class PersonPhoneNumberController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Creates new phone number for person
    /// </summary>
    /// <param name="request.name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost(Name = nameof(CreatePersonPhoneNumber))]
    public async Task<Unit> CreatePersonPhoneNumber([FromBody] CreatePersonPhoneNumberModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToCreatePersonPhoneNumberCommand(), cancellationToken);

    /// <summary>
    /// Deletes phonenumber by personId nad phoneNumberId
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(nameof(DeletePersonPhoneNumber))]
    public async Task DeletePersonPhoneNumber([FromBody] DeletePersonPhoneNumberModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToDeletePersonPhoneNumberCommand(), cancellationToken);

    /// <summary>
    /// Updates person phone number
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(nameof(UpdatePersonPhoneNumber))]
    public async Task UpdatePersonPhoneNumber([FromBody] UpdatePersonPhoneNumberModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToUpdatePersonPhoneNumberCommand(), cancellationToken);
}
