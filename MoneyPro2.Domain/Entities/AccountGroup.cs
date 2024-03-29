﻿using Flunt.Notifications;
using Flunt.Validations;

namespace MoneyPro2.Domain.Entities;
public class AccountGroup : Notifiable<Notification>
{
    public AccountGroup(int usuarioId, string? apelido, string? descricao)
    {
        GrupoContaId = 0;
        UsuarioId = usuarioId;
        Apelido = apelido;
        Descricao = descricao;

        AccountGroupContracts();
    }

    public int GrupoContaId { get; private set; }
    public int UsuarioId { get; private set; }
    public User User { get; set; } = null!;
    public string? Apelido { get; private set; } = null!;
    public string? Descricao { get; private set; } = null!;
    public int? Ordem { get; private set; }
    public bool? Ativo { get; private set; } = true;
    public bool? Painel { get; private set; } = false;
    public bool? FluxoDisponivel { get; private set; } = false;
    public bool? FluxoCredito { get; private set; } = false;

    public void SetApelido(string? apelido)
    {
        Apelido = apelido;
        AccountGroupContracts();
    }

    public void SetDescricao(string? descricao)
    {
        Descricao = descricao;
        AccountGroupContracts();
    }

    private void AccountGroupContracts()
    {
        Clear();
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsTrue(
                    Apelido?.Length >= 1 && Apelido?.Length <= 40,
                    "Apelido",
                    "O apelido deve conter de 1 a 40 caracteres"
                )
                .IsTrue(
                    Descricao?.Length >= 1 && Descricao?.Length <= 100,
                    "Descricao",
                    "A descrição deve ter de 1 até 100 caracteres"
                )
        );
    }

}
