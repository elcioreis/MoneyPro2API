using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using MoneyPro2.API.Extensions;
using MoneyPro2.API.Models;
using MoneyPro2.API.ViewModels;
using MoneyPro2.API.ViewModels.Users;

namespace MoneyPro2.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("v1/users/")]
    public async Task<IActionResult> NewUserAsync([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var user = new User(model.Username, model.Nome, model.Email, model.CPF, model.Senha);

        if (user.IsValid)
        {
            return Ok();
        }
        else
        {
            return BadRequest(new ResultViewModel<List<Notification>>(user.Notifications.ToList()));
        }
    }
}
