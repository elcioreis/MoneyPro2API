using MoneyPro2.Domain.Entities;

namespace MoneyPro2.Test.Models;

[TestClass]
public class UserTest
{
    private readonly string _username = "jose";
    private readonly string _nome = "José da Silva";
    private readonly string _email = "jose.silva@gmail.com";
    private readonly string _cpf = "509.254.178-40";
    private readonly string _senha = "ABCabc123!@#";
    [TestMethod]
    public void Usuario_Com_Username_Nulo_Deve_Falhar()
    {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
        var user = new User(null, _nome, _email, _cpf, _senha);
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
        Assert.IsFalse(user.IsValid);
    }
    [TestMethod]
    public void Usuario_Com_Username_Invalido_Deve_Falhar()
    {
        var badUsername = "Jose Silva";
        var user = new User(badUsername, _nome, _email, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }
    [TestMethod]
    public void Usuario_Com_Nome_Nulo_Deve_Falhar()
    {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
        var user = new User(_username, null, _email, _cpf, _senha);
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
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
    public void Usuario_Com_Email_Nulo_Deve_Falhar()
    {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
        var user = new User(_username, _nome, null, _cpf, _senha);
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_Email_Invalido_Deve_Falhar()
    {
        var badEmail = "josesilvaATgmail.com";
        var user = new User(_username, _nome, badEmail, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_Email_Longo_Deve_Falhar()
    {
        var badEmail = _email + "." + new string('m', 200);
        var user = new User(_username, _nome, badEmail, _cpf, _senha);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_CPF_Nulo_Deve_Falhar()
    {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
        var user = new User(_username, _nome, _email, null, _senha);
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    public void Usuario_Com_CPF_Invalido_Deve_Falhar()
    {
        var badCPF = "509.254.178-99";
        var user = new User(_username, _nome, _email, badCPF, _senha);
        Assert.IsFalse(user.IsValid);
    }
    [TestMethod]
    public void Usuario_Com_Senha_Nula_Deve_Falhar()
    {
#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
        var user = new User(_username, _nome, _email, _cpf, null);
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
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