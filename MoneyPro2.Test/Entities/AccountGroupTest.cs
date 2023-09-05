using MoneyPro2.Domain.Entities;

namespace MoneyPro2.Test.Entities;

[TestClass]
public class AccountGroupTest
{
    private readonly int _userId = 1;
    private readonly string _apelido = "Disponível";
    private readonly string _descricao = "Recursos disponíveis";

    [TestMethod]
    [TestCategory("AccountGroup")]
    public void Grupo_de_contas_valido_deve_passar()
    {
        var accountGroup = new AccountGroup(_userId, _apelido, _descricao);
        Assert.IsTrue(accountGroup.IsValid);
    }

    [TestMethod]
    [TestCategory("AccountGroup")]
    public void Grupo_de_contas_com_apelido_invalido_deve_falhar()
    {
        var badApelido = new string('x', 1000);
        var accountGroup = new AccountGroup(_userId, badApelido, _descricao);
        Assert.IsFalse(accountGroup.IsValid);
    }

    [TestMethod]
    [TestCategory("AccountGroup")]
    public void Grupo_de_contas_com_apelido_nulo_deve_falhar()
    {
        string? badApelido = null;
        var accountGroup = new AccountGroup(_userId, badApelido, _descricao);
        Assert.IsFalse(accountGroup.IsValid);
    }

    [TestMethod]
    [TestCategory("AccountGroup")]
    public void Grupo_de_contas_com_descricao_invalida_deve_falhar()
    {
        var badDescricao = new string('x', 1000);
        var accountGroup = new AccountGroup(_userId, _apelido, badDescricao);
        Assert.IsFalse(accountGroup.IsValid);
    }

    [TestMethod]
    [TestCategory("AccountGroup")]
    public void Grupo_de_contas_com_descricao_nula_deve_falhar()
    {
        string? badDescricao = null;
        var accountGroup = new AccountGroup(_userId, _apelido, badDescricao);
        Assert.IsFalse(accountGroup.IsValid);
    }
}
