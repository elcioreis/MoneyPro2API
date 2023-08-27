using Flunt.Notifications;
using Flunt.Validations;

namespace MoneyPro2.Domain.Entities;

public class Institution : Notifiable<Notification>
{
    public Institution() { }
    public Institution(int userId, int tipoInstituicaoId, string apelido, string descricao)
    {
        InstituicaoId = 0;
        UserId = userId;
        TipoInstituicaoId = tipoInstituicaoId;
        Apelido = apelido;
        Descricao = descricao;
        InstitutionContracts();
    }

    public int InstituicaoId { get; private set; }
    public int UserId { get; private set; }
    public int TipoInstituicaoId { get; private set; }
    public string Apelido { get; private set; } = null!;
    public string Descricao { get; private set; } = null!;
    public bool? Ativo { get; private set; } = true;
    public User User { get; set; } = null!;
    public InstitutionType InstitutionType { get; set; } = null!;

    public bool SetTipoInsituicao(int tipoInstituicaoId)
    {
        TipoInstituicaoId = tipoInstituicaoId;
        InstitutionContracts();
        return IsValid;
    }

    public bool SetApelido(string apelido)
    {
        Apelido = apelido;
        InstitutionContracts();
        return IsValid;
    }

    public bool SetDescricao(string descricao)
    {
        Descricao = descricao;
        InstitutionContracts();
        return IsValid;
    }

    private void InstitutionContracts()
    {
        Clear();
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsTrue(
                    Apelido?.Length >= 1 && Apelido?.Length <= 40,
                    "Apelido",
                    "O apelido deve conter entre 1 e 40 caracteres"
                )
                .IsTrue(
                    Descricao?.Length >= 1 && Descricao?.Length <= 100,
                    "Descrição",
                    "A descrição deve conter entre 1 e 100 caracteres"
                )
        );
    }
}
