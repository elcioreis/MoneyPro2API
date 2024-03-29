﻿using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data;
using MoneyPro2.API.Extensions;
using MoneyPro2.API.ViewModels;
using MoneyPro2.API.ViewModels.Coin;
using MoneyPro2.Domain.Entities;

namespace MoneyPro2.API.Controllers;

[ApiController]
public class CoinController : ControllerBase
{
    [Authorize]
    [HttpGet("v1/coin")]
    public async Task<IActionResult> GetAsync([FromServices] MoneyPro2DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var coins = await context.Coins
                .AsNoTracking()
                .Select(
                    x =>
                        new
                        {
                            x.MoedaId,
                            x.Apelido,
                            x.Simbolo,
                            x.Padrao,
                            x.MoedaVirtual,
                            x.BancoCentral,
                            x.Eletronica,
                            x.Observacao
                        }
                )
                .OrderByDescending(x => x.Padrao)
                .ThenBy(x => x.Apelido)
                .ToListAsync();
            return Ok(new ResultViewModel<dynamic>(coins));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("04x01 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpGet("v1/coin/default")]
    public async Task<IActionResult> GetDefaultAsync([FromServices] MoneyPro2DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var coin = await context.Coins
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Padrao == true);

            if (coin == null)
                return NotFound(new ResultViewModel<string>("04x02 - Conteúdo não localizado"));

            return Ok(new ResultViewModel<dynamic>(coin));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("04x03 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpGet("v1/coin/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        try
        {
            var coin = await context.Coins
                .AsNoTracking()
                .Where(x => x.MoedaId == id)
                .FirstOrDefaultAsync();

            if (coin == null)
                return NotFound(new ResultViewModel<string>("04x04 - Conteúdo não localizado"));

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        coin.MoedaId,
                        coin.Apelido,
                        coin.Simbolo,
                        coin.Padrao,
                        coin.MoedaVirtual,
                        coin.BancoCentral,
                        coin.Eletronica,
                        coin.Observacao
                    }
                )
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("04x05 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPost("v1/coin")]
    public async Task<IActionResult> PostAsync(
        [FromBody] CoinViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var coins = await context.Coins.CountAsync();

        var coin = new Coin(
            model.Apelido?.Trim(),
            model.Simbolo?.Trim(),
            coins == 0,
            model.MoedaVirtual,
            model.BancoCentral,
            model.Eletronica,
            model.Observacao?.Trim()
        );

        if (!coin.IsValid)
            return BadRequest(new ResultViewModel<List<Notification>>(coin.Notifications.ToList()));

        try
        {
            await context.Coins.AddAsync(coin);
            await context.SaveChangesAsync();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        coin.MoedaId,
                        coin.Apelido,
                        coin.Simbolo,
                        coin.Padrao,
                        coin.MoedaVirtual,
                        coin.BancoCentral,
                        coin.Eletronica,
                        coin.Observacao
                    }
                )
            );
        }
        catch (DbUpdateException ex)
        {
            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_coin_apelido")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"04x06 - O apelido '{coin.Apelido}' já está em uso"
                    )
                );

            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_coin_simbolo")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"04x07 - O símbolo '{coin.Simbolo}' já está em uso"
                    )
                );

            return StatusCode(
                500,
                new ResultViewModel<string>("04x08 - Erro ao cadastrar a moeda")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("04x09 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPut("v1/coin/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] CoinViewModel model,
        [FromServices] MoneyPro2DataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var coin = await context.Coins.FirstOrDefaultAsync(x => x.MoedaId == id);

        if (coin == null)
            return NotFound(new ResultViewModel<string>("04x0A - Informação não localizada"));

        coin.SetApelido(model.Apelido?.Trim());
        coin.SetSimbolo(model.Simbolo?.Trim());
        coin.SetMoedaVirtual(model.MoedaVirtual);
        coin.SetBancoCentral(model.BancoCentral);
        coin.SetEletronica(model.Eletronica);
        coin.SetObservacao(model.Observacao?.Trim());

        if (!coin.IsValid)
            return BadRequest(new ResultViewModel<List<Notification>>(coin.Notifications.ToList()));

        try
        {
            context.Coins.Update(coin);
            await context.SaveChangesAsync();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        coin.MoedaId,
                        coin.Apelido,
                        coin.Simbolo,
                        coin.Padrao,
                        coin.MoedaVirtual,
                        coin.BancoCentral,
                        coin.Eletronica,
                        coin.Observacao
                    }
                )
            );
        }
        catch (DbUpdateException ex)
        {
            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_coin_apelido")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"04x0B - O apelido '{coin.Apelido}' já está em uso"
                    )
                );

            if (
                ex.InnerException != null
                && ex.InnerException.Message.ToLower().Contains("ix_coin_simbolo")
            )
                return StatusCode(
                    500,
                    new ResultViewModel<string>(
                        $"04x0C - O símbolo '{coin.Simbolo}' já está em uso"
                    )
                );

            return StatusCode(
                500,
                new ResultViewModel<string>("04x0D - Erro ao atualizar a moeda")
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("04x0E - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpDelete("v1/coin/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var coin = await context.Coins.FirstOrDefaultAsync(x => x.MoedaId == id);

        if (coin == null)
            return NotFound(new ResultViewModel<string>("04x0F - Informação não localizada"));

        try
        {
            context.Coins.Remove(coin);
            await context.SaveChangesAsync();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        coin.MoedaId,
                        coin.Apelido,
                        coin.Simbolo,
                        coin.Padrao,
                        coin.MoedaVirtual,
                        coin.BancoCentral,
                        coin.Eletronica,
                        coin.Observacao
                    }
                )
            );
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<string>("04x10 - Erro ao excluir a moeda"));
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("04x12 - Erro interno no servidor")
            );
        }
    }

    [Authorize]
    [HttpPut("v1/coin/setdefault/{id:int}")]
    public async Task<IActionResult> PutSetDefaultAsync(
        [FromRoute] int id,
        [FromServices] MoneyPro2DataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErros()));

        var newDefault = await context.Coins.FirstOrDefaultAsync(x => x.MoedaId == id);

        if (newDefault == null)
            return NotFound(new ResultViewModel<string>("04x13 - Informação não localizada"));

        var coins = await context.Coins.Where(x => x.Padrao == true).ToListAsync();

        using var transaction = context.Database.BeginTransaction();

        try
        {
            foreach (var coin in coins)
            {
                // Remove o "Padrao" de outra moeda qualquer
                coin.SetPadrao(false);
                context.Update(coin);
            }

            // Marca a nova como "Padrao"
            newDefault.SetPadrao(true);
            context.Update(newDefault);
            await context.SaveChangesAsync();

            transaction.Commit();

            return Ok(
                new ResultViewModel<dynamic>(
                    new
                    {
                        newDefault.MoedaId,
                        newDefault.Apelido,
                        newDefault.Simbolo,
                        newDefault.Padrao,
                        newDefault.MoedaVirtual,
                        newDefault.BancoCentral,
                        newDefault.Eletronica,
                        newDefault.Observacao
                    }
                )
            );
        }
        catch (DbUpdateException)
        {
            transaction.Rollback();
            return StatusCode(
                500,
                new ResultViewModel<dynamic>("04x14 - Erro interno no servidor")
            );
        }
    }
}
