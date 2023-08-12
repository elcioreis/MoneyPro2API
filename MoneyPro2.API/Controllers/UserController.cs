﻿using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data;
using MoneyPro2.API.Extensions;
using MoneyPro2.API.Models;
using MoneyPro2.API.ViewModels;
using MoneyPro2.API.ViewModels.Users;
using MoneyPro2.Shared.Functions;

namespace MoneyPro2.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("v1/login/")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        string cripto = Tools.GenerateMD5(model.Username, model.Senha);

        var user = context.Users
            .AsNoTracking()
            .FirstOrDefault(x => x.Username == model.Username && x.Criptografada == cripto);

        if (user == null)
        {
            return Unauthorized(new ResultViewModel<string>("01x01 - Usuário ou senha incorretos"));
        }

        try
        {
            await context.UserLogins.AddAsync(new UserLogin { UserId = user.UserId });
            await context.SaveChangesAsync();
        }
        catch
        {
            return StatusCode(
                500,
                new ResultViewModel<string>("01x02 - Não foi possível logar")
            );
        }

        return Ok(
            new ResultViewModel<ResultUserViewModel>(
                new ResultUserViewModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email.Address
                }
            )
        );
        //return StatusCode(401, new ResultViewModel<string>("01x01 - Usuário ou senha incorretos"));
    }

    [HttpPost("v1/users/")]
    public async Task<IActionResult> NewUserAsync(
        [FromBody] RegisterViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var user = new User(model.Username, model.Nome, model.Email, model.CPF, model.Senha);

        if (!user.IsValid)
        {
            return BadRequest(new ResultViewModel<List<Notification>>(user.Notifications.ToList()));
        }

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        userid = user.UserId,
                        username = user.Username,
                        email = user.Email.Address
                    }
                )
            );
        }
        catch (DbUpdateException ex)
        {
            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_user_username")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"00x02 - O usuário '{user.Username}' já está em uso"
                    )
                );

            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_user_email")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"00x03 - O e-mail '{user.Email.Address}' já está em uso"
                    )
                );

            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_user_cpf")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"00x04 - O CPF '{user.CPF.Conteudo}' já está em uso"
                    )
                );

            return StatusCode(
                500,
                new ResultViewModel<string>("00x05 - Erro ao cadastrar o usuário")
            );
        }
        catch
        {
            return StatusCode(
                500,
                new ResultViewModel<string>("00x01 - Falha interna no servidor")
            );
        }
    }
}
