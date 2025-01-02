using Application.Notification;
using Application.Queries.GenerateCode.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class GenerateCodesController : MainController
{
    private IMediator _mediator;

    public GenerateCodesController(INotificationError notificationError, IMediator mediator) : base(notificationError)
    {
        _mediator = mediator;
    }

    [HttpGet("gerar-api/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
         return View(await _mediator.Send(new QueryGenerateCodeGetByIdRequest(id)));
        //return View();
    }
}