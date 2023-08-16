using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data;
using MoneyPro2.API.Extensions;
using MoneyPro2.API.Models;
using MoneyPro2.API.ViewModels;
using MoneyPro2.API.ViewModels.Institution;

namespace MoneyPro2.API.Controllers;

[ApiController]
public class InstitutionController : ControllerBase
{
    [Authorize]
    [HttpGet("v1/institution")]
    public async Task<IActionResult> GetAsync([FromServices] MoneyPro2DataContext context)
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var institutions = await context.Institutions
                .AsNoTracking()
                .Where(x => x.UserId == userid)
                .Select(
                    x =>
                        new
                        {
                            x.InstituicaoId,
                            x.TipoInstituicaoId,
                            x.Apelido,
                            x.Descricao,
                            x.Ativo
                        }
                )
                .OrderBy(x => x.Apelido)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(institutions));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x01 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpGet("v1/institution/{institutionId:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int institutionId,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var institutions = await context.Institutions
                .AsNoTracking()
                .Where(x => x.InstituicaoId == institutionId && x.UserId == userid)
                .Select(
                    x =>
                        new
                        {
                            x.InstituicaoId,
                            x.TipoInstituicaoId,
                            x.Apelido,
                            x.Descricao,
                            x.Ativo
                        }
                )
                .OrderBy(x => x.Apelido)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(institutions));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x02 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpGet("v1/institution/institutiontype/{institutionTypeId:int}")]
    public async Task<IActionResult> GetByTypeIdAsync(
        [FromRoute] int institutionTypeId,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var institutions = await context.Institutions
                .AsNoTracking()
                .Where(x => x.UserId == userid && x.TipoInstituicaoId == institutionTypeId)
                .Select(
                    x =>
                        new
                        {
                            x.InstituicaoId,
                            x.TipoInstituicaoId,
                            x.Apelido,
                            x.Descricao,
                            x.Ativo
                        }
                )
                .OrderBy(x => x.Apelido)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(institutions));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x03 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPost("v1/institution")]
    public async Task<IActionResult> PostAsync(
        [FromBody] InstitutionViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var institution = new Institution(
            userid,
            model.TipoInstituicaoId,
            model.Apelido,
            model.Descricao
        );

        if (!institution.IsValid)
        {
            return BadRequest(
                new ResultViewModel<List<Notification>>(institution.Notifications.ToList())
            );
        }

        try
        {
            await context.Institutions.AddAsync(institution);
            await context.SaveChangesAsync();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        institution.InstituicaoId,
                        institution.TipoInstituicaoId,
                        institution.Apelido,
                        institution.Descricao,
                        institution.Ativo
                    }
                )
            );
        }
        catch (DbUpdateException ex)
        {
            if (
                ex.InnerException != null
                && ex.InnerException.Message
                    .ToLower()
                    .Contains("ix_instituicao_userid_tipoinstituicaoid_apelido")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"03x04 - O apelido '{institution.Apelido}' já está em uso para este tipo de instituição"
                    )
                );

            return StatusCode(
                500,
                new ResultViewModel<string>("03x05 - Erro ao cadastrar a instituição")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x06 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPut("v1/institution/{institutionId:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int institutionId,
        [FromBody] InstitutionViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var institution = await context.Institutions.FirstOrDefaultAsync(
            x => x.InstituicaoId == institutionId && x.UserId == userid
        );

        if (institution == null)
        {
            return NotFound(new ResultViewModel<string>("03x07 - Informação não localizada"));
        }

        institution.SetApelido(model.Apelido);
        institution.SetDescricao(model.Descricao);

        if (!institution.IsValid)
        {
            return BadRequest(
                new ResultViewModel<List<Notification>>(institution.Notifications.ToList())
            );
        }

        try
        {
            context.Institutions.Update(institution);
            await context.SaveChangesAsync();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        institution.InstituicaoId,
                        institution.TipoInstituicaoId,
                        institution.Apelido,
                        institution.Descricao,
                        institution.Ativo
                    }
                )
            );
        }
        catch (DbUpdateException ex)
        {
            if (
                ex.InnerException != null
                && ex.InnerException.Message
                    .ToLower()
                    .Contains("ix_instituicao_userid_tipoinstituicaoid_apelido")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"03x08 - O apelido '{institution.Apelido}' já está em uso para este tipo de instituição"
                    )
                );

            return StatusCode(
                500,
                new ResultViewModel<string>("03x09 - Erro ao cadastrar a instituição")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x10 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpDelete("v1/institution/{institutionId:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int institutionId,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        var institution = await context.Institutions.FirstOrDefaultAsync(
            x => x.InstituicaoId == institutionId && x.UserId == userid
        );

        if (institution == null)
        {
            return NotFound(new ResultViewModel<string>("03x11 - Informação não localizada"));
        }

        try
        {
            context.Institutions.Remove(institution);
            await context.SaveChangesAsync();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        institution.InstituicaoId,
                        institution.TipoInstituicaoId,
                        institution.Apelido,
                        institution.Descricao,
                        institution.Ativo
                    }
                )
            );
        }
        catch (DbUpdateException)
        {
            return StatusCode(
                500,
                new ResultViewModel<string>("03x12 - Erro ao excluir a instituição")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x13 - Erro interno no servidor")
            );
        }
    }
}
