﻿using Flunt.Notifications;
using Flunt.Validations;
using MoneyPro2.Domain.Functions;
using MoneyPro2.Shared.ValueObjects;

namespace MoneyPro2.Domain.ValueObjects;

public class CPF : ValueObject
{
    public CPF(string conteudo)
    {
        if (!string.IsNullOrEmpty(conteudo))
            Conteudo = conteudo.Trim().Replace(".", "").Replace("-", "");

        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsTrue(Tools.CheckCPF(Conteudo), "CPF", "CPF inválido")
        );
    }

    public string Conteudo { get; private set; } = string.Empty;
}
