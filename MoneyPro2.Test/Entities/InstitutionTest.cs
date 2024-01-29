using MoneyPro2.Domain.Entities;

namespace MoneyPro2.Test.Entities;

[TestClass]
public class InstitutionTest
{
    private readonly int _userId = 1;
    private readonly string? _tipoInstituicaoId = "1";
    private readonly string? _apelido = "Esquina";
    private readonly string? _descricao = "Banco da Esquina SA";

    [TestMethod]
    [TestCategory("Institution")]
    public void Instituicao_valida_deve_passar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        Assert.IsTrue(institution.IsValid);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Instituicao_com_apelido_nulo_deve_falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, null, _descricao);
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Instituicao_com_apelido_invalido_deve_falhar()
    {
        var badApelido = new string('x', 100);
        var institution = new Institution(_userId, _tipoInstituicaoId, badApelido, _descricao);
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Instituicao_com_descricao_nula_deve_falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, null);
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Instituicao_com_descricao_invalida_deve_falhar()
    {
        var badDescricao = new string('x', 101);
        Institution institution = new(
            _userId,
            _tipoInstituicaoId,
            _apelido,
            badDescricao
        );
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Instituicao_com_apelido_alterado_invalido_deve_falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        institution.SetApelido(new string('x', 45));
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Instituicao_com_descricao_alterada_invalida_deve_falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        institution.SetDescricao(new string('x', 109));
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Instituicao_com_tipo_de_instituicao_alterada_invalida_deve_falhar()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        institution.SetTipoInsituicao("TESTE");
        Assert.IsFalse(institution.IsValid);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Inativar_Instituicao_deve_deixar_inativa()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        institution.SetInactive();
        Assert.IsTrue(institution.IsValid && institution.Ativo == false);
    }

    [TestMethod]
    [TestCategory("Institution")]
    public void Ativar_Instituicao_deve_deixar_ativa()
    {
        var institution = new Institution(_userId, _tipoInstituicaoId, _apelido, _descricao);
        // Inativa primeiro
        institution.SetInactive();
        // Depois reativa
        institution.SetActive();
        Assert.IsTrue(institution.IsValid && institution.Ativo == true);
    }
}
