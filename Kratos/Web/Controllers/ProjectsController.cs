using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProjectsController : Controller
    {
        [HttpGet("lista-de-projetos")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("consultar-projeto")]
        public IActionResult GetById(int id)
        {
            return View();
        }
    }
}
