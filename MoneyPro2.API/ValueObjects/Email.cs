﻿using Flunt.Notifications;
using Flunt.Validations;
using MoneyPro2.Shared.ValueObjects;

namespace MoneyPro2.API.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        if (!string.IsNullOrEmpty(address))
            Address = address.Trim().ToLower();

        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsEmail(Address, "Email", "E-mail inválido")
                .IsTrue(Address.Length <= 200, "Email", "E-mail deve ter até 200 caracteres")
        );
    }

    public string Address { get; private set; } = string.Empty;

    public override string ToString()
    {
        return this.Address;
    }
}
