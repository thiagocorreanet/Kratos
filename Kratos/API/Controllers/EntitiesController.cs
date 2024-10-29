using Application.Commands.Entitie.Delete;
using Application.Commands.Entity.Create;
using Application.Commands.Entity.Update;
using Application.Commands.EntityProperty.Delete;
using Application.Notification;
using Application.Queries;
using Application.Queries.Entitie.GetById;
using Application.Queries.EntitiyModel.GetAll;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/entities")]
    public class EntitiesController : MainController
    {
        readonly IMediator _mediator;

        public EntitiesController(INotificationError notificationError, IMediator mediator) : base(notificationError)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<QueryEntityGetAllResponse>>> GetAll()
        {
            return Ok(await _mediator.Send(new QueryEntityGetAllRequest()));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<QueryEntityGetByIdResponse>> GetById(int id)
        {
            var entitie = await _mediator.Send(new QueryEntityGetByIdRequest(id: id));

            if(entitie is null)
                return NotFound();

            return Ok(entitie);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateEntityCommandRequest request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _mediator.Send(request);

            if (!ValidOperation()) return CustomResponse();

            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UpdateEntityCommandRequest request)
        {
            if (id != request.Id)
            {
                NotifyError("Oops! We cannot process your request due to ID integrity errors.");
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
            await _mediator.Send(new DeleteEntityCommandRequest(id));

            if (!ValidOperation()) return CustomResponse();

            return NoContent();
        }
    }
}
