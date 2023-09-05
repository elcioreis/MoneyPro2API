using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data;
using MoneyPro2.API.Extensions;
using MoneyPro2.API.ViewModels;
using MoneyPro2.API.ViewModels.Account;
using MoneyPro2.Domain.Entities;

namespace MoneyPro2.API.Controllers;

[ApiController]
public class AccountGroupController : ControllerBase
{
    [Authorize]
    [HttpGet("v1/accountgroup")]
    public async Task<IActionResult> GetAsync([FromServices] MoneyPro2DataContext context)
    {
        int userId = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var accountgroups = await context.AccountGroups
                .AsNoTracking()
                .Where(x => x.UsuarioId == userId)
                .Select(
                    x =>
                        new
                        {
                            x.GrupoContaId,
                            x.Apelido,
                            x.Descricao,
                            x.Ordem,
                            x.Ativo,
                            x.Painel,
                            x.FluxoDisponivel,
                            x.FluxoCredito
                        }
                )
                .OrderBy(x => x.Apelido)
                .ToListAsync();
            return Ok(new ResultViewModel<dynamic>(accountgroups));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("05x01 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpGet("v1/accountgroup/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userId = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var accountGroup = await context.AccountGroups
                .AsNoTracking()
                .Where(x => x.UsuarioId == userId && x.GrupoContaId == id)
                .Select(
                    x =>
                        new
                        {
                            x.GrupoContaId,
                            x.Apelido,
                            x.Descricao,
                            x.Ordem,
                            x.Ativo,
                            x.Painel,
                            x.FluxoDisponivel,
                            x.FluxoCredito
                        }
                )
                .FirstOrDefaultAsync();

            if (accountGroup == null)
                return NotFound(new ResultViewModel<string>("05x02 - Conteúdo não localizado"));

            return Ok(new ResultViewModel<dynamic>(accountGroup));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("05x03 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPost("v1/accountgroup")]
    public async Task<IActionResult> PostAsync(
        [FromBody] AccountGroupViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userId = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var accountGroup = new AccountGroup(userId, model.Apelido, model.Descricao);

        if (!accountGroup.IsValid)
        {
            return BadRequest(
                new ResultViewModel<List<Notification>>(accountGroup.Notifications.ToList())
            );
        }

        try
        {
            await context.AccountGroups.AddAsync(accountGroup);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(accountGroup));
        }
        catch (DbUpdateException ex)
        {
            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_accountgroup_usuarioid_apelido")
            )
            {
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"05x04 - O apelido '{accountGroup.Apelido}' já está em uso"
                    )
                );
            }

            return StatusCode(
                500,
                new ResultViewModel<string>("05x05 - Erro ao incluir o grupo de contas")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("05x06 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPut("v1/accountgroup/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] AccountGroupViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userId = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var accountGroup = await context.AccountGroups.FirstOrDefaultAsync(x => x.UsuarioId == userId && x.GrupoContaId == id);

        if (accountGroup == null)
            return NotFound(new ResultViewModel<string>("05x07 - Informação não localizada"));

        accountGroup.SetApelido(model.Apelido);
        accountGroup.SetDescricao(model.Descricao);

        if (!accountGroup.IsValid)
        {
            return BadRequest(
                new ResultViewModel<List<Notification>>(accountGroup.Notifications.ToList())
            );
        }

        try
        {
            context.AccountGroups.Update(accountGroup);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(accountGroup));
        }
        catch (DbUpdateException ex)
        {
            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_accountgroup_usuarioid_apelido")
            )
            {
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"05x08 - O apelido '{accountGroup.Apelido}' já está em uso"
                    )
                );
            }

            return StatusCode(
                500,
                new ResultViewModel<string>("05x09 - Erro ao alterar o grupo de contas")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("05x0A - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpDelete("v1/accountgroup/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userId = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var accountGroup = await context.AccountGroups.FirstOrDefaultAsync(x => x.UsuarioId == userId && x.GrupoContaId == id);

        if (accountGroup == null)
            return NotFound(new ResultViewModel<string>("05x0B - Informação não localizada"));

        try
        {
            context.AccountGroups.Remove(accountGroup);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(accountGroup));
        }
        catch (DbUpdateException)
        {
            return StatusCode(
                500,
                new ResultViewModel<string>("05x0C - Erro ao excluir o grupo de contas")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("05x0D - Erro interno no servidor")
            );
        }
    }
}
