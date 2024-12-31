using Application.Commands.TypeData.Create;
using Application.Commands.TypeData.Delete;
using Application.Commands.TypeData.Update;
using Application.Notification;
using Application.Queries.TypeData.GetAll;
using Application.Queries.TypeData.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class TypeDataController : MainController
{
    private IMediator _mediator;

    public TypeDataController(INotificationError notificationError, IMediator mediator) : base(notificationError)
    {
        _mediator = mediator;
    }

    [HttpGet("lista-de-tipos")]
    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new QueryTypeDataGetAllRequest()));
    }
    
    [HttpGet("consultar-tipo/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return View(await _mediator.Send(new QueryTypeDataGetByIdRequest(id)));
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTypeDataCommandRequest request)
    {
        if (!ModelState.IsValid)
            return CustomJsonResponse(ModelState, true);

        var result = await _mediator.Send(request);

        if (result is false)
        {
            return Json(new { success = false });
        }

        return Json(new
        {
            success = true
        });

    }
    
    [HttpPut("alterar-tipo/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTypeDataCommandRequest request)
    {
        if (!ModelState.IsValid)
            return CustomJsonResponse(ModelState, true);

        var result = await _mediator.Send(request);

        if (result is false)
        {
            return Json(new { success = false });
        }

        return Json(new
        {
            success = true
        });

    }
    
    [HttpDelete("excluir-tipo/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
            return CustomJsonResponse(ModelState, true);

        var result = await _mediator.Send(new DeleteTypeDataCommandRequest(id));

        if (result is false)
        {
            return Json(new { success = false });
        }

        return Json(new
        {
            success = true
        });

    }
}