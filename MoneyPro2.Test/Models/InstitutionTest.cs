using MoneyPro2.API.Models;

namespace MoneyPro2.Test.Models;

[TestClass]
public class InstitutionTest
{
    private readonly int _userId = 1;
    private readonly int _tipoInstituicaoId = 1;
    private readonly string _apelido = "Esquina";
    private readonly string _descricao = "Banco da Esquina SA";

    [TestMethod]
    public void Instituicao_Com_Apelido_Nulo_Deve_Falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, null, _descricao);
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    public void Instituicao_Com_Apelido_Invalido_Deve_Falhar()
    {
        var badApelido = new string('x', 41);
        var institution = new Institution(_userId, _tipoInstituicaoId, badApelido, _descricao);
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    public void Instituicao_Com_Descricao_Nula_Deve_Falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, null);
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    public void Instituicao_Com_Descricao_Invalida_Deve_Falhar()
    {
        var badDescricao = new string('x', 101);
        Institution institution = new Institution(
            _userId,
            _tipoInstituicaoId,
            _apelido,
            badDescricao
        );
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    public void Instituicao_Valida_Deve_Passar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        Assert.IsTrue(institution.IsValid);
    }

    [TestMethod]
    public void Insituicao_Com_Apelido_Alterado_Invalido_Deve_Falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        institution.SetApelido(new string('x', 45));
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    public void Insituicao_Com_Descricao_Alterada_Invalida_Deve_Falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        institution.SetDescricao(new string('x', 109));
        Assert.IsFalse(institution.IsValid);
    }
}
