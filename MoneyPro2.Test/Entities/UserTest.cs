using MoneyPro2.Domain.Entities;

namespace MoneyPro2.Test.Entities;

[TestClass]
public class UserTest
{
    private readonly string _username = "jose";
    private readonly string _nome = "José da Silva";
    private readonly string _email = "jose.silva@gmail.com";
    private readonly string _cpf = "509.254.178-40";
    private readonly string _senha = "ABCabc123!@#";


    [TestMethod]
    [TestCategory("User")]
    public void Usuario_valido_deve_pasar()
    {
        var user = new User(_username, _nome, _email, _cpf, _senha);
        Assert.IsTrue(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_username_nulo_deve_falhar()
    {
        var user = new User(null, _nome, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_username_invalido_deve_falhar()
    {
        var badUsername = "Jose Silva";
        var user = new User(badUsername, _nome, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_nome_nulo_deve_falhar()
    {
        var user = new User(_username, null, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_nome_curto_deve_falhar()
    {
        var badName = "J";
        var user = new User(_username, badName, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_nome_longo_deve_falhar()
    {
        var badName = new string('n', 75);
        var user = new User(_username, badName, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_email_nulo_deve_falhar()
    {
        var user = new User(_username, _nome, null, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_email_invalido_deve_falhar()
    {
        var badEmail = "josesilvaATgmail.com";
        var user = new User(_username, _nome, badEmail, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_email_longo_deve_falhar()
    {
        var badEmail = _email + "." + new string('m', 200);
        var user = new User(_username, _nome, badEmail, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_cpf_nulo_deve_falhar()
    {
        var user = new User(_username, _nome, _email, null, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_cpf_invalido_deve_falhar()
    {
        var badCPF = "509.254.178-99";
        var user = new User(_username, _nome, _email, badCPF, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_senha_nula_deve_falhar()
    {
        var user = new User(_username, _nome, _email, _cpf, null);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_senha_fraca_deve_falhar()
    {
        var badSenha = "123456";
        var user = new User(_username, _nome, _email, _cpf, badSenha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [TestCategory("User")]
    public void Usuario_com_senha_invalida_deve_falhar()
    {
        var badSenha = "";
        var user = new User(_username, _nome, _email, _cpf, badSenha);
        Assert.IsFalse(user.IsValid);
    }

}
