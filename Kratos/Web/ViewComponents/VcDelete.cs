using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents;

public class VcDelete : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync() => View();
}
