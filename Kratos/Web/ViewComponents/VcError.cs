using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents;

public class VcError : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync() => View();
}
