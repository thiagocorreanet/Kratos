using Application.Commands.Entitie.Delete;
using Application.Commands.Entity.Create;
using Application.Commands.Entity.Update;
using Application.Commands.Project.Update;
using Application.Notification;
using Application.Queries.EntitiyModel.GetAll;
using Application.Queries.EntitiyModel.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class EntitiesController : MainController
    {
        private IMediator _mediator;

        public EntitiesController(INotificationError notificationError, IMediator mediator) : base(notificationError)
        {
            _mediator = mediator;
        }

        [HttpGet("lista-de-entidades")]
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new QueryEntityGetAllRequest()));
        }

        [HttpGet("consultar-entidade/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return View(await _mediator.Send(new QueryEntityGetByIdRequest(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateEntityCommandRequest request)
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

        [HttpPut("alterar-entidade/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEntityCommandRequest request)
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

        [HttpDelete("excluir-entidade/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return CustomJsonResponse(ModelState, true);

            var result = await _mediator.Send(new DeleteEntityCommandRequest(id));

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
}
