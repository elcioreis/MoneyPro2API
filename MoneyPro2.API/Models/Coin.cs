using Flunt.Notifications;
using Flunt.Validations;

namespace MoneyPro2.API.Models;

public class Coin : Notifiable<Notification>
{
    public Coin(
        string apelido,
        string simbolo,
        string padrao,
        string moedaVirtual,
        string bancoCentral,
        string observacao,
        string eletronica
    )
    {
        if (bancoCentral != null)
        {
            int numeroBancoCentral;
            if (int.TryParse(bancoCentral, out numeroBancoCentral))
                BancoCentral = numeroBancoCentral;
            else
                BancoCentral = 0;
        }
        else
        {
            BancoCentral = null;
        }

        Apelido = apelido;
        Simbolo = simbolo;

        if (padrao?.ToLower() == "s")
            Padrao = true;
        else if (padrao?.ToLower() == "n")
            Padrao = false;
        else
            Padrao = null;

        if (moedaVirtual?.ToLower() == "s")
            MoedaVirtual = true;
        else if (moedaVirtual?.ToLower() == "n")
            MoedaVirtual = false;
        else
            MoedaVirtual = null;

        Observacao = observacao;
        Eletronica = eletronica;

#pragma warning disable CS8604 // Possível argumento de referência nula.
        CoinContracts();
#pragma warning restore CS8604 // Possível argumento de referência nula.
    }

    public int MoedaId { get; private set; }
    public string Apelido { get; private set; } = string.Empty;
    public string Simbolo { get; private set; } = string.Empty;
    public bool? Padrao { get; private set; }
    public bool? MoedaVirtual { get; private set; }
    public int? BancoCentral { get; private set; }
    public string? Observacao { get; private set; } = string.Empty;
    public string? Eletronica { get; private set; } = string.Empty;

    public bool SetApelido(string apelido)
    {
        Apelido = apelido;
        //CoinContracts();
        return IsValid;
    }

    private void CoinContracts()
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
                    Simbolo?.Length >= 1 && Simbolo?.Length <= 10,
                    "Apelido",
                    "O apelido deve conter de 1 a 40 caracteres"
                )
                .IsTrue(
                    Padrao != null,
                    "Padrão",
                    "O padrão deve ser (S)im ou (N)ão"
                )
                .IsTrue(
                    MoedaVirtual != null,
                    "MoedaVirtual",
                    "MoedaVirtual deve ser (S)im ou (N)ão"
                )
                .IsTrue(
                    BancoCentral == null || BancoCentral > 0,
                    "BancoCentral",
                    "O banco central deve ser um número maior que zero ou nulo"
                )
                .IsTrue(
                    Observacao == null || Observacao?.Length <= 50,
                    "Observacao",
                    "A observação deve ter até 50 caracteres ou ser nula"
                )
                .IsTrue(
                    Eletronica == null || Eletronica?.Length <= 10,
                    "Eletronica",
                    "O símbolo da moeda eletrônica deve ter até 10 caracteres ou ser nulo"
                )
        );
    }
}
