using MoneyPro2.API.Models;

namespace MoneyPro2.Test.Models;
[TestClass]
public class InstitutionTypeTest
{
    private readonly int _userId = 1;
    private readonly string _apelido = "Banco";
    private readonly string _descricao = "Instituições bancárias";

    [TestMethod]
    public void Tipo_De_Instituicao_Com_Apelido_Nulo_Deve_Falhar()
    {
        InstitutionType institutionType = new InstitutionType(_userId, null, _descricao);
        Assert.IsFalse(institutionType.IsValid);
    }

    [TestMethod]
    public void Tipo_De_Instituicao_Com_Apelido_Invalido_Deve_Falhar()
    {
        string badApelido = new string('m', 120);
        InstitutionType institutionType = new InstitutionType(_userId, badApelido, _descricao);
        Assert.IsFalse(institutionType.IsValid);
    }
    [TestMethod]
    public void Tipo_De_Instituicao_Com_Descricao_Nula_Deve_Falhar()
    {
        InstitutionType institutionType = new InstitutionType(_userId, _apelido, null);
        Assert.IsFalse(institutionType.IsValid);
    }

    [TestMethod]
    public void Tipo_De_Instituicao_Com_Descricao_Invalida_Deve_Falhar()
    {
        string badDescricao = new string('m', 120);
        InstitutionType institutionType = new InstitutionType(_userId, _apelido, badDescricao);
        Assert.IsFalse(institutionType.IsValid);
    }
    [TestMethod]
    public void Tipo_De_Instituicao_Valida_Deve_Passar()
    {
        InstitutionType institutionType = new InstitutionType(_userId, _apelido, _descricao);
        Assert.IsTrue(institutionType.IsValid);
    }
}
