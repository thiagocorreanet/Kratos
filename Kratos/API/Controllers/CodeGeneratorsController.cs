using Application.Notification;
using Application.Queries;
using Application.Queries.Entitie.GetById;
using Application.Queries.GenerateCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CodeGeneratorsController : MainController
{
    private readonly IMediator _mediator;
    
    public CodeGeneratorsController(INotificationError notificationError, IMediator mediator) : base(notificationError)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<string>> GetById(int id)
    {
        var entitie = await _mediator.Send(new QueryGenerateCodeRequest());

        if(entitie is null)
            return NotFound();

        return Ok(entitie);
    }
}