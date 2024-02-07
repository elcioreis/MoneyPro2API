using Microsoft.AspNetCore.Mvc;
using MoneyPro2.API.Data;
using MoneyPro2.API.ViewModels;
using MoneyPro2.API.ViewModels.Status;

namespace MoneyPro2.API.Controllers;

public class StatusController : Controller
{
    [HttpPost("v1/status/")]
    public async Task<IActionResult> Status([FromServices] MoneyPro2DataContext context)
    {
        var status = new StatusViewModel("API em execução");
        return Ok(new ResultViewModel<StatusViewModel>(status));
    }
}
