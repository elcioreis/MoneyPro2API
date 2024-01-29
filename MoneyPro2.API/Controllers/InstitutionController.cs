using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data;
using MoneyPro2.API.Extensions;
using MoneyPro2.API.ViewModels;
using MoneyPro2.API.ViewModels.Institution;
using MoneyPro2.Domain.Entities;

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
    [HttpGet("v1/institution/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var institution = await context.Institutions
                .AsNoTracking()
                .Where(x => x.InstituicaoId == id && x.UserId == userid)
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
                .FirstOrDefaultAsync();

            if (institution == null)
                return NotFound(new ResultViewModel<String>("03x02 - Conteúdo não localizado"));

            return Ok(new ResultViewModel<dynamic>(institution));
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
    [HttpGet("v1/institution/institutiontype/{id:int}")]
    public async Task<IActionResult> GetByTypeIdAsync(
        [FromRoute] int id,
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
                .Where(x => x.UserId == userid && x.TipoInstituicaoId == id)
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
                new ResultViewModel<dynamic>("03x04 - Erro interno no servidor")
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

        int tipoId = 0;

        if (!int.TryParse(model.TipoInstituicaoId, out tipoId))
        {
            return StatusCode(
                500,
                new ResultViewModel<string>($"03x05 - O tipo de instituição informado é inválido")
            );
        }

        var institutionType = await context.InstitutionTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TipoInstituicaoId == tipoId);

        if (institutionType == null)
        {
            return StatusCode(
                500,
                new ResultViewModel<string>($"03x06 - O tipo de institução não foi encontrado")
            );
        }

        if (institutionType.UserId != userid)
        {
            return StatusCode(
                500,
                new ResultViewModel<string>(
                    $"03x07 - O tipo de institução não pertence a este usuário"
                )
            );
        }

        if (institutionType.Ativo == false)
        {
            return StatusCode(
                500,
                new ResultViewModel<string>($"03x08 - O tipo de institução não está ativo")
            );
        }

        var institution = new Institution(
            userid,
            model.TipoInstituicaoId,
            model.Apelido?.Trim(),
            model.Descricao?.Trim()
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
                        $"03x09 - O apelido '{institution.Apelido}' já está em uso para este tipo de instituição"
                    )
                );

            return StatusCode(
                500,
                new ResultViewModel<string>("03x0A - Erro ao cadastrar a instituição")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x0B - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPut("v1/institution/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] InstitutionViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        int tipoId = 0;

        if (!int.TryParse(model.TipoInstituicaoId, out tipoId))
        {
            return StatusCode(
                500,
                new ResultViewModel<string>($"03x0C - O tipo de instituição informado é inválido")
            );
        }

        var institutionType = await context.InstitutionTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TipoInstituicaoId == tipoId);

        if (institutionType == null)
        {
            return StatusCode(
                500,
                new ResultViewModel<string>($"03x0D - O tipo de institução não foi encontrado")
            );
        }

        if (institutionType.UserId != userid)
        {
            return StatusCode(
                500,
                new ResultViewModel<string>(
                    $"03x0E - O tipo de institução não pertence a este usuário"
                )
            );
        }

        if (institutionType.Ativo == false)
        {
            return StatusCode(
                500,
                new ResultViewModel<string>($"03x0F - O tipo de institução não está ativo")
            );
        }

        var institution = await context.Institutions.FirstOrDefaultAsync(
            x => x.InstituicaoId == id
        );

        if (institution == null)
        {
            return NotFound(new ResultViewModel<string>("03x10 - Conteúdo não localizado"));
        }

        institution.SetTipoInsituicao(model.TipoInstituicaoId);
        institution.SetApelido(model.Apelido?.Trim());
        institution.SetDescricao(model.Descricao?.Trim());

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
            var msg = ex.InnerException?.Message.ToLower() ?? String.Empty;

            if (msg.Contains("ix_instituicao_userid_tipoinstituicaoid_apelido"))
            {
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"03x11 - O apelido '{institution.Apelido}' já está em uso por outra instituição"
                    )
                );
            }

            if (msg.Contains("fk_instituicao_tipoinstituicao_tipoinstituicaoid"))
            {
                return StatusCode(
                    500,
                    new ResultViewModel<string>($"03x12 - O tipo de instituição não foi encontrado")
                );
            }

            return StatusCode(
                500,
                new ResultViewModel<string>("03x13 - Erro ao atualizar a instituição")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x14 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpDelete("v1/institution/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        var institution = await context.Institutions.FirstOrDefaultAsync(
            x => x.InstituicaoId == id && x.UserId == userid
        );

        if (institution == null)
        {
            return NotFound(new ResultViewModel<string>("03x15 - Conteúdo não localizado"));
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
        catch (DbUpdateException ex)
        {
            var msg = ex.InnerException?.Message.ToLower() ?? String.Empty;

            if (msg.Contains("xyzxyzxyz"))
            {
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"03x16 - A instituição está em uso por alguma conta"
                    )
                );
            }

            return StatusCode(
                500,
                new ResultViewModel<string>("03x17 - Erro ao excluir a instituição")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x18 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPut("v1/institution/inactivate/{id:int}")]
    public async Task<IActionResult> PutInactivateAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var institution = context.Institutions.FirstOrDefault(
            x => x.InstituicaoId == id && x.UserId == userid
        );

        if (institution == null)
            return NotFound(new ResultViewModel<string>("03x19 - Conteúdo não localizado"));

        institution.SetInactive();

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
        catch (DbUpdateException)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>(
                    "03x1A - Não foi possível inativar a instituição"
                )
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x1B - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPut("v1/institution/activate/{id:int}")]
    public async Task<IActionResult> PutActivateAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        int userid = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var institution = context.Institutions.FirstOrDefault(
            x => x.InstituicaoId == id && x.UserId == userid
        );

        if (institution == null)
            return NotFound(new ResultViewModel<string>("03x1C - Conteúdo não localizado"));

        institution.SetActive();

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
        catch (DbUpdateException)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>(
                    "03x1D - Não foi possível ativar a instituição"
                )
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("03x1E - Erro interno no servidor")
            );
        }
    }
}