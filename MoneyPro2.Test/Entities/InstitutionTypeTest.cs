using MoneyPro2.Domain.Entities;

namespace MoneyPro2.Test.Entities;
[TestClass]
public class InstitutionTypeTest
{
    private readonly int _userId = 1;
    private readonly string _apelido = "Banco";
    private readonly string _descricao = "Instituições bancárias";

    [TestMethod]
    [TestCategory("InstitutionType")]

    public void Tipo_de_instituicao_valida_deve_passar()
    {
        var institutionType = new InstitutionType(_userId, _apelido, _descricao);
        Assert.IsTrue(institutionType.IsValid);
    }

    [TestMethod]
    [TestCategory("InstitutionType")]
    public void Tipo_de_instituicao_com_apelido_nulo_deve_falhar()
    {
        var institutionType = new InstitutionType(_userId, null, _descricao);
        Assert.IsFalse(institutionType.IsValid);
    }

    [TestMethod]
    [TestCategory("InstitutionType")]

    public void Tipo_de_instituicao_com_apelido_invalido_deve_falhar()
    {
        var badApelido = new string('m', 120);
        var institutionType = new InstitutionType(_userId, badApelido, _descricao);
        Assert.IsFalse(institutionType.IsValid);
    }

    [TestMethod]
    [TestCategory("InstitutionType")]

    public void Tipo_de_instituicao_com_descricao_nula_deve_falhar()
    {
        var institutionType = new InstitutionType(_userId, _apelido, null);
        Assert.IsFalse(institutionType.IsValid);
    }

    [TestMethod]
    [TestCategory("InstitutionType")]

    public void Tipo_de_instituicao_com_descricao_invalida_deve_falhar()
    {
        var badDescricao = new string('m', 120);
        var institutionType = new InstitutionType(_userId, _apelido, badDescricao);
        Assert.IsFalse(institutionType.IsValid);
    }


    [TestMethod]
    [TestCategory("InstitutionType")]

    public void Tipo_de_insituicao_com_apelido_alterado_invalido_deve_falhar()
    {
        var institutionType = new InstitutionType(_userId, _apelido, _descricao);
        institutionType.SetApelido("");
        Assert.IsFalse(institutionType.IsValid);
    }

    [TestMethod]
    [TestCategory("InstitutionType")]

    public void Tipo_de_insituicao_com_descricao_alterada_invalida_deve_falhar()
    {
        var institutionType = new InstitutionType(_userId, _apelido, _descricao);
        institutionType.SetDescricao("");
        Assert.IsFalse(institutionType.IsValid);
    }

    [TestMethod]
    [TestCategory("InstitutionType")]
    public void Inativar_Tipo_de_instituicao_deve_deixar_inativa()
    {
        var institutionType = new InstitutionType(_userId, _apelido, _descricao);
        institutionType.SetInactive();
        Assert.IsTrue(institutionType.IsValid && institutionType.Ativo == false);
    }

    [TestMethod]
    [TestCategory("InstitutionType")]
    public void Ativar_Tipo_de_instituicao_deve_deixar_ativa()
    {
        var institutionType = new InstitutionType(_userId, _apelido, _descricao);
        // Inativa primeiro
        institutionType.SetInactive();
        // Depois reativa
        institutionType.SetActive();
        Assert.IsTrue(institutionType.IsValid && institutionType.Ativo == true);
    }
}
