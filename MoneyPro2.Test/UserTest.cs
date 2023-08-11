using MoneyPro2.API.Models;
using MoneyPro2.API.ValueObjects;

namespace MoneyPro2.Test;

[TestClass]
public class UserTest
{
    // public User(string username, string nome, Email email, CPF cpf, string senha)
    private readonly string _username = "jose";
    private readonly string _nome = "Jose Silva";
    private readonly Email _email = new("jose.silva@gmail.com");
    private readonly CPF _cpf = new("509.254.178-40");
    private readonly string _senha = "ABCabc123!@#";

    [TestMethod]
    public void Usuario_Com_Username_Invalido_Deve_Falhar()
    {
        var badUsername = "Jose Silva";
        var user = new User(badUsername, _nome, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_Nome_Curto_Deve_Falhar()
    {
        var badName = "J";
        var user = new User(_username, badName, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_Nome_Longo_Deve_Falhar()
    {
        var badName = new string('n', 75);
        var user = new User(_username, badName, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_Email_Invalido_Deve_Falhar()
    {
        var badEmail = new Email("josesilvaATgmail.com");
        var user = new User(_username, _nome, badEmail, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_CPF_Invalido_Deve_Falhar()
    {
        var badCPF = new CPF("509.254.178-99");
        var user = new User(_username, _nome, _email, badCPF, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_Senha_Fraca_Deve_Falhar()
    {
        var badSenha = "123456";
        var user = new User(_username, _nome, _email, _cpf, badSenha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_Senha_Invalida_Deve_Falhar()
    {
        var badSenha = "";
        var user = new User(_username, _nome, _email, _cpf, badSenha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Valido_Deve_Pasar()
    {
        var user = new User(_username, _nome, _email, _cpf, _senha);
        Assert.IsTrue(user.IsValid);
    }
}