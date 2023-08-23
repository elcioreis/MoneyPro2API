using MoneyPro2.API.Models;

namespace MoneyPro2.Test.Models;

[TestClass]
public class CoinTest
{
    public readonly string _apelido = "Real";
    public readonly string _simbolo = "R$";
    public readonly string _padraoSim = "S";
    public readonly string _padraoNao = "N";
    public readonly string _moedaVirtualSim = "S";
    public readonly string _moedaVirtualNao = "N";
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
    public readonly string _bancoCentral = null;
    public readonly string _eletronica = null;
    public readonly string _observacao = null;
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.

    [TestMethod]
    public void Moeda_Valida_ComPadrao_SIM_e_MoedaVirtual_SIM_Deve_Passar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualSim,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsTrue(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Valida_ComPadrao_SIM_e_MoedaVirtual_NAO_Deve_Passar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsTrue(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Valida_ComPadrao_NAO_e_MoedaVirtual_SIM_Deve_Passar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoNao,
            _moedaVirtualSim,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsTrue(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Valida_ComPadrao_NAO_e_MoedaVirtual_NAO_Deve_Passar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoNao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsTrue(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Valida_Com_BancoCentral_Maior_Que_Zero_Deve_Passar()
    {
        var bancoCentral = "100";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoNao,
            _moedaVirtualNao,
            bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsTrue(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Apelido_Invalido_Deve_Falhar()
    {
        var badApelido = new string('x', 100);
        var coin = new Coin(
            badApelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Apelido_Nulo_Deve_Falhar()
    {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
        string nullApelido = null;
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

#pragma warning disable CS8604 // Possível argumento de referência nula.
        var coin = new Coin(
            nullApelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
#pragma warning restore CS8604 // Possível argumento de referência nula.
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Simbolo_Invalido_Deve_Falhar()
    {
        var badSimbolo = new string('x', 100);
        var coin = new Coin(
            _apelido,
            badSimbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Simbolo_Nulo_Deve_Falhar()
    {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
        string nullSimbolo = null;
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

#pragma warning disable CS8604 // Possível argumento de referência nula.
        var coin = new Coin(
            _apelido,
            nullSimbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
#pragma warning restore CS8604 // Possível argumento de referência nula.
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Padrao_Invalido_Deve_Falhar()
    {
        var badPadrao = "YES";
        var coin = new Coin(
            _apelido,
            _simbolo,
            badPadrao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Padrao_Nulo_Deve_Falhar()
    {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
        string nullPadrao = null;
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

#pragma warning disable CS8604 // Possível argumento de referência nula.
        var coin = new Coin(
            _apelido,
            _simbolo,
            nullPadrao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
#pragma warning restore CS8604 // Possível argumento de referência nula.
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_MoedaVirtual_Invalida_Deve_Falhar()
    {
        var badMoedaVirtual = "YES";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            badMoedaVirtual,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_MoedaVirtual_Nula_Deve_Falhar()
    {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
        string nullMoedaVirtual = null;
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

#pragma warning disable CS8604 // Possível argumento de referência nula.
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            nullMoedaVirtual,
            _bancoCentral,
            _eletronica,
            _observacao
        );
#pragma warning restore CS8604 // Possível argumento de referência nula.

        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_BancoCentral_Com_Numero_Invalido_Deve_Falhar()
    {
        var badCancoCentral = "XXX";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            badCancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_BancoCentral_Igual_A_Zero_Deve_Falhar()
    {
        var badCancoCentral = "0";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            badCancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_BancoCentral_Menor_Que_Zero_Deve_Falhar()
    {
        var badCancoCentral = "-10";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            badCancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Observacao_Invalida_Deve_Falhar()
    {
        var badObservacao = new string('x', 60);
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            badObservacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_CodigoEletronica_Invalido_Deve_Falhar()
    {
        var badEletronica = new string('x', 11);
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            badEletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Apelido_Alterado_Invalido_Deve_Falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        var badApelido = new string('x', 100);
        coin.SetApelido(badApelido);
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Simbolo_Alterado_Invalido_Deve_Falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        var badSimbolo = new string('x', 100);
        coin.SetSimbolo(badSimbolo);
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Padrao_Alterado_Invalido_Deve_Falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        var badPadrao = "Y";
        coin.SetPadrao(badPadrao);
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_MoedaVirtual_Alterado_Invalido_Deve_Falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        var badMoedaVirtual = "Y";
        coin.SetMoedaVirtual(badMoedaVirtual);
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_BancoCentral_Alterado_Invalido_Deve_Falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        var badBancoCentral = "CEM";
        coin.SetBancoCentral(badBancoCentral);
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Eletronica_Alterada_Invalido_Deve_Falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        var badEletronica = new string('x', 1000);
        coin.SetEletronica(badEletronica);
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    public void Moeda_Com_Observacao_Alterado_Invalido_Deve_Falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padraoSim,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        var badObservacao = new string('x', 1000);
        coin.SetEletronica(badObservacao);
        Assert.IsFalse(coin.IsValid);
    }
}
