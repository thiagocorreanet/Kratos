using Application.Commands.EntityProperty.Create;
using Application.Commands.EntityProperty.Delete;
using Application.Commands.EntityProperty.Update;
using Application.Notification;
using Application.Queries.EntityProperty;
using Application.Queries.EntityProperty.GetAll;
using Application.Queries.EntityProperty.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/entities-property")]
public class EntitiesPropertiesController : MainController
{
    private readonly IMediator _mediator;
    
    public EntitiesPropertiesController(INotificationError notificationError, IMediator mediator) : base(notificationError)
    {
        _mediator = mediator;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<QueryEntityPropertyGetAllResponse>>> GetAll()
    {
        return Ok(await _mediator.Send(new QueryEntityPropertyGetAllRequest()));
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<QueryEntityPropertyGetByIdResponse>> GetById(int id)
    {
        var entitieProperty = await _mediator.Send(new QueryEntityPropertyGetByIdRequest(id));

        if(entitieProperty is null)
            return NotFound();

        return Ok(entitieProperty);
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateEntityPropertyRequest request)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _mediator.Send(request);

        if (!ValidOperation()) return CustomResponse();

        return Created();
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, UpdateEntityPropertyRequest request)
    {
        if (id != request.Id)
        {
            NotifyError(message: "Oops! We cannot process your request due to ID integrity errors.");
            return CustomResponse();
        }
            
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _mediator.Send(request);

        if (!ValidOperation()) return CustomResponse();

        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteEntityPropertyRequest(id));

        if (!ValidOperation()) return CustomResponse();

        return NoContent();
    }
}