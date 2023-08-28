using Flunt.Notifications;
using Flunt.Validations;

namespace MoneyPro2.Domain.Entities;

public class Coin : Notifiable<Notification>
{
    public Coin() { }

    public Coin(
        string? apelido,
        string? simbolo,
        string? moedaVirtual,
        string? bancoCentral,
        string? eletronica,
        string? observacao
    )
    {
        SetApelido(apelido);
        SetSimbolo(simbolo);
        Padrao = false;

        if (moedaVirtual?.ToLower() == "s")
            MoedaVirtual = true;
        else if (moedaVirtual?.ToLower() == "n")
            MoedaVirtual = false;
        else
            MoedaVirtual = null;

        SetBancoCentral(bancoCentral);

        Eletronica = eletronica;
        Observacao = observacao;

        CoinContracts();
    }

    public int MoedaId { get; private set; }
    public string? Apelido { get; private set; } = string.Empty;
    public string? Simbolo { get; private set; } = string.Empty;
    public bool Padrao { get; private set; } = false;
    public bool? MoedaVirtual { get; private set; } = false;
    public int? BancoCentral { get; private set; }
    public string? Eletronica { get; private set; } = string.Empty;
    public string? Observacao { get; private set; } = string.Empty;

    public void SetApelido(string? apelido)
    {
        Apelido = apelido;
        CoinContracts();
    }

    public void SetSimbolo(string? simbolo)
    {
        Simbolo = simbolo;
        CoinContracts();
    }

    public void SetPadrao(bool padrao)
    {
        Padrao = padrao;
    }

    public void SetMoedaVirtual(string moedaVirtual)
    {
        if (moedaVirtual?.ToLower() == "s")
            MoedaVirtual = true;
        else if (moedaVirtual?.ToLower() == "n")
            MoedaVirtual = false;
        else
            MoedaVirtual = null;

        CoinContracts();
    }

    public void SetBancoCentral(string? bancoCentral)
    {
        if (bancoCentral != null)
        {
            if (int.TryParse(bancoCentral, out int numeroBancoCentral))
                BancoCentral = numeroBancoCentral;
            else
                BancoCentral = 0;
        }
        else
        {
            BancoCentral = null;
        }
        CoinContracts();
    }

    public void SetEletronica(string eletronica)
    {
        Eletronica = eletronica;
        CoinContracts();
    }

    public void SetObservacao(string observacao)
    {
        Observacao = observacao;
        CoinContracts();
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
                    "Simbolo",
                    "O símbolo deve conter de 1 a 10 caracteres"
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
                    Eletronica == null || Eletronica?.Length <= 10,
                    "Eletronica",
                    "O símbolo da moeda eletrônica deve ter até 10 caracteres ou ser nulo"
                )
                .IsTrue(
                    Observacao == null || Observacao?.Length <= 50,
                    "Observacao",
                    "A observação deve ter até 50 caracteres ou ser nula"
                )
        );
    }
}
