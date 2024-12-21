using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents;

public class VcSuccess : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync() => View();
}
