﻿using MoneyPro2.Domain.Entities;

namespace MoneyPro2.Test.Entities;

[TestClass]
public class CoinTest
{
    public readonly string _apelido = "Real";
    public readonly string _simbolo = "R$";
    public readonly bool _padrao = true;
    public readonly string _moedaVirtualSim = "S";
    public readonly string _moedaVirtualNao = "N";
    public readonly string? _bancoCentral = null;
    public readonly string? _eletronica = null;
    public readonly string? _observacao = null;

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_valida_com_moedaVirtual_SIM_deve_passar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            _moedaVirtualSim,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsTrue(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_valida_com_moedaVirtual_NAO_deve_passar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsTrue(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_valida_com_bancoCentral_maior_que_zero_deve_passar()
    {
        var bancoCentral = "100";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsTrue(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_apelido_invalido_deve_falhar()
    {
        var badApelido = new string('x', 100);
        var coin = new Coin(
            badApelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_apelido_nulo_deve_falhar()
    {
        string? nullApelido = null;

        var coin = new Coin(
            nullApelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_simbolo_invalido_deve_falhar()
    {
        var badSimbolo = new string('x', 100);
        var coin = new Coin(
            _apelido,
            badSimbolo,
            _padrao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_simbolo_nulo_deve_falhar()
    {
        string? nullSimbolo = null;

        var coin = new Coin(
            _apelido,
            nullSimbolo,
            _padrao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_moedaVirtual_invalida_deve_falhar()
    {
        var badMoedaVirtual = "YES";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            badMoedaVirtual,
            _bancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_moedaVirtual_nula_deve_falhar()
    {
        string? nullMoedaVirtual = null;

        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            nullMoedaVirtual,
            _bancoCentral,
            _eletronica,
            _observacao
        );

        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_bancoCentral_com_numero_invalido_deve_falhar()
    {
        var badCancoCentral = "XXX";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            badCancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_bancoCentral_igual_a_zero_deve_falhar()
    {
        var badCancoCentral = "0";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            badCancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_bancoCentral_menor_que_zero_deve_falhar()
    {
        var badCancoCentral = "-10";
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            badCancoCentral,
            _eletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_observacao_invalida_deve_falhar()
    {
        var badObservacao = new string('x', 60);
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            _bancoCentral,
            _eletronica,
            badObservacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_codigoEletronica_invalido_deve_falhar()
    {
        var badEletronica = new string('x', 11);
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
            _moedaVirtualNao,
            _bancoCentral,
            badEletronica,
            _observacao
        );
        Assert.IsFalse(coin.IsValid);
    }

    [TestMethod]
    [TestCategory("Coin")]
    public void Moeda_com_apelido_alterado_invalido_deve_falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
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
    [TestCategory("Coin")]
    public void Moeda_com_simbolo_alterado_invalido_deve_falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
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
    [TestCategory("Coin")]
    public void Moeda_com_moedaVirtual_alterado_invalido_deve_falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
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
    [TestCategory("Coin")]
    public void Moeda_com_bancoCentral_alterado_invalido_deve_falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
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
    [TestCategory("Coin")]
    public void Moeda_com_eletronica_alterada_invalido_deve_falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
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
    [TestCategory("Coin")]
    public void Moeda_com_observacao_alterado_invalido_deve_falhar()
    {
        var coin = new Coin(
            _apelido,
            _simbolo,
            _padrao,
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
