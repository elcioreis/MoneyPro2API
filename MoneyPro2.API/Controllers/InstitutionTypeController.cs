using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data;
using MoneyPro2.API.Extensions;
using MoneyPro2.API.Models;
using MoneyPro2.API.ViewModels;
using MoneyPro2.API.ViewModels.InstitutionType;

namespace MoneyPro2.API.Controllers;

[ApiController]
public class InstitutionTypeController : ControllerBase
{
    [Authorize]
    [HttpGet("v1/institutiontype")]
    public async Task<IActionResult> GetAsync([FromServices] MoneyPro2DataContext context)
    {
        int userid = User.GetUserId();

        try
        {
            var institutionTypes = await context.InstitutionTypes
                .AsNoTracking()
                .Where(x => x.UserId == userid)
                .Select(
                    x =>
                        new
                        {
                            x.TipoInstituicaoId,
                            x.Apelido,
                            x.Descricao,
                            x.Ativo
                        }
                )
                .OrderBy(x => x.Apelido)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(institutionTypes));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("02x01 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpGet("v1/institutiontype/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        try
        {
            var resultSet = await context.InstitutionTypes
                .AsNoTracking()
                .Where(x => x.UserId == userid)
                .Select(
                    x =>
                        new
                        {
                            x.TipoInstituicaoId,
                            x.Apelido,
                            x.Descricao,
                            x.Ativo
                        }
                )
                .FirstOrDefaultAsync(x => x.TipoInstituicaoId == id);

            if (resultSet != null)
            {
                return Ok(new ResultViewModel<dynamic>(resultSet));
            }
            else
            {
                return NotFound(new ResultViewModel<string>("02x02 - Conteúdo inexistente"));
            }
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("02x03 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPost("v1/institutiontype")]
    public async Task<IActionResult> PostAsync(
        [FromBody] InstitutionTypeViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

#pragma warning disable CS8604 // Possível argumento de referência nula.
        var institutionType = new InstitutionType(userid, model.Apelido, model.Descricao);
#pragma warning restore CS8604 // Possível argumento de referência nula.

        if (!institutionType.IsValid)
        {
            return BadRequest(
                new ResultViewModel<List<Notification>>(institutionType.Notifications.ToList())
            );
        }

        try
        {
            await context.InstitutionTypes.AddAsync(institutionType);
            await context.SaveChangesAsync();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        institutionType.TipoInstituicaoId,
                        institutionType.Apelido,
                        institutionType.Descricao,
                        institutionType.Ativo
                    }
                )
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("02x04 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPut("v1/institutiontype/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] InstitutionTypeViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var institutionType = context.InstitutionTypes.FirstOrDefault(
            x => x.TipoInstituicaoId == id && x.UserId == userid
        );

        if (institutionType == null)
            return NotFound(new ResultViewModel<string>("02x05 - Conteúdo inexistente"));

#pragma warning disable CS8604 // Possível argumento de referência nula.
        institutionType.SetApelido(model.Apelido);
        institutionType.SetDescricao(model.Descricao);
#pragma warning restore CS8604 // Possível argumento de referência nula.

        if (!institutionType.IsValid)
        {
            return BadRequest(
                new ResultViewModel<List<Notification>>(institutionType.Notifications.ToList())
            );
        }

        try
        {
            context.InstitutionTypes.Update(institutionType);
            await context.SaveChangesAsync();
            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        institutionType.TipoInstituicaoId,
                        institutionType.Apelido,
                        institutionType.Descricao,
                        institutionType.Ativo
                    }
                )
            );
        }
        catch (DbUpdateException ex)
        {
            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_tipoinstituicao_userid_apelido")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"02x06 - O apelido '{institutionType.Apelido}' já está em uso"
                    )
                );

            return StatusCode(
                500,
                new ResultViewModel<dynamic>(
                    "02x07 - Não foi possível atualizar o tipo de instituição"
                )
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("02x08 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpDelete("v1/institutiontype/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var institutionType = context.InstitutionTypes.FirstOrDefault(
            x => x.TipoInstituicaoId == id && x.UserId == userid
        );

        if (institutionType == null)
            return NotFound(new ResultViewModel<string>("02x09 - Conteúdo inexistente"));

        try
        {
            context.InstitutionTypes.Remove(institutionType);
            await context.SaveChangesAsync();
            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        institutionType.TipoInstituicaoId,
                        institutionType.Apelido,
                        institutionType.Descricao,
                        institutionType.Ativo
                    }
                )
            );
        }
        catch (DbUpdateException)
        {
            ;
            return StatusCode(
                500,
                new ResultViewModel<dynamic>(
                    "02x10 - Não foi possível excluir o tipo de instituição"
                )
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("02x11 - Erro interno no servidor")
            );
        }
    }
}
