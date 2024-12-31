using Application.Commands.EntityProperty.Create;
using Application.Commands.EntityProperty.Delete;
using Application.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

    public class EntitiesPropertiesController : MainController
    {
        private readonly IMediator _mediator;

        public EntitiesPropertiesController(INotificationError notificationError, IMediator mediator) : base(notificationError)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateEntityPropertyRequest request)
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
        
        [HttpDelete("excluir-propriedade/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return CustomJsonResponse(ModelState, true);

            var result = await _mediator.Send(new DeleteEntityPropertyRequest(id));

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
