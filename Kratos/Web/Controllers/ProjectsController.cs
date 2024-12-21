using Application.Commands.Project.Create;
using Application.Notification;
using Application.Queries.Project.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProjectsController : MainController
    {
        private IMediator _mediator;

        public ProjectsController(INotificationError notificationError, IMediator mediator) : base(notificationError)
        {
            _mediator = mediator;
        }

        [HttpGet("lista-de-projetos")]
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new QueryProjectGetAllRequest()));
        }

        [HttpGet("consultar-projeto")]
        public IActionResult GetById(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProjectCommandRequest request)
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
    }
}
