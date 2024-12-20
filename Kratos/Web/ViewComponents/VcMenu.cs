using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents;

public class VcMenu : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync() => View();
}
