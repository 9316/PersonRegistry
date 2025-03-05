using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonRegistry.Application.Person.Commands.Create;
using PersonRegistry.Application.Person.Commands.DeletePhoto;
using PersonRegistry.Application.Person.Commands.Update;
using PersonRegistry.Application.Person.Commands.UploadPhoto;
using PersonRegistry.Application.Person.Queries.DownloadPersonImage;
using PersonRegistry.Application.Person.Queries.GetPerson;
using PersonRegistry.Application.Person.Queries.GetPersonRelationReport;
using PersonRegistry.Application.Person.Queries.GetPersons;
using PersonRegistry.Common.Application.Paging;
using PersonRegistry.API.Mappers;
using PersonRegistry.Application.Person.Commands.Delete;

namespace PersonRegistry.API.Controllers;

/// <summary>
/// Controller for Persons
/// </summary>
/// <param name="_mediator"></param>
[Route("api/[controller]")]
[ApiController]
public class PersonController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Get person by Id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(nameof(GetPersonById))]
    public async Task<GetPersonModelResponse> GetPersonById([FromQuery] GetPersonModelRequest request, CancellationToken cancellationToken)
        => await _mediator.Send(request.ToPersonQuery(), cancellationToken);

    /// <summary>
    /// Get all persons with filter
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(nameof(GetPersons))]
    public async Task<PagedResult<GetPersonsModelResponse>> GetPersons([FromQuery] GetPersonsModelRequest request, CancellationToken cancellationToken)
        => await _mediator.Send(request.ToGetPersonQuery(), cancellationToken);


    /// <summary>
    /// Get persons report by relation type
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(nameof(GetPersonRelationReport))]
    public async Task<List<PersonRelationReportModelResponse>> GetPersonRelationReport(CancellationToken cancellationToken)
        => await _mediator.Send(new GetPersonRelationReportQuery(), cancellationToken);

    /// <summary>
    /// Download person's photo by url
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(nameof(DownloadPersonPhoto))]
    public async Task<IActionResult> DownloadPersonPhoto([FromQuery] DownloadPersonImageModelRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request.ToDownloadPersonImageCommand(), cancellationToken);
        return File(response, "image/jpeg");
    }

    /// <summary>
    /// Creates new Person
    /// </summary>
    /// <param name="request.name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost(Name = nameof(PostPerson))]
    public async Task<int> PostPerson([FromBody] CreatePersonModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToCreatePersonCommand(), cancellationToken);

    /// <summary>
    /// Updates Person by Id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(nameof(UpdatePerson))]
    public async Task UpdatePerson([FromBody] UpdatePersonModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToUpdatePersonCommand(), cancellationToken);

    /// <summary>
    /// Deletes person by Id
    /// </summary>
    /// <param name="request.Id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(nameof(DeletePerson))]
    public async Task DeletePerson([FromBody] DeletePersonModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToDeletePersonCommand(), cancellationToken);

    /// <summary>
    /// Upload person photo by personId with photo's file
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>        
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost(nameof(UploadPersonPhoto))]
    public async Task<Unit> UploadPersonPhoto([FromForm] UploadPersonPhotoModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(new UploadPersonPhotoCommand(request.PersonId, request.Photo), cancellationToken);

    /// <summary>
    /// Delete person's photo by personId
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(nameof(DeletePersonPhoto))]
    public async Task DeletePersonPhoto([FromBody] DeletePersonPhotoModelRequest request, CancellationToken cancellationToken) =>
        await _mediator.Send(request.ToDeletePersonPhotoCommand(), cancellationToken);
}
