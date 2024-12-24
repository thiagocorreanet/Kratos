using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents;

public class VcEntityProperties : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
