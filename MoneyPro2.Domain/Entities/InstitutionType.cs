using Flunt.Notifications;
using Flunt.Validations;

namespace MoneyPro2.Domain.Entities;

public class InstitutionType : Notifiable<Notification>
{
    public InstitutionType() { }

    public InstitutionType(int userId, string apelido, string descricao)
    {
        TipoInstituicaoId = 0;
        UserId = userId;
        Apelido = apelido;
        Descricao = descricao;
        Ativo = true;

        InstitutionTypeContracts();
    }

    public int UserId { get; private set; }
    public int TipoInstituicaoId { get; private set; }
    public string Apelido { get; private set; } = null!;
    public string Descricao { get; private set; } = null!;
    public bool? Ativo { get; private set; } = true;
    public User User { get; set; } = null!;
    public ICollection<Institution> Institutions { get; private set; } = new List<Institution>();

    public bool SetApelido(string apelido)
    {
        Apelido = apelido;
        InstitutionTypeContracts();
        return IsValid;
    }

    public bool SetDescricao(string descricao)
    {
        Descricao = descricao;
        InstitutionTypeContracts();
        return IsValid;
    }

    private void InstitutionTypeContracts()
    {
        Clear();
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                //.IsNotNull(Apelido, "Apelido", "O apelido não pode ser nulo")
                //.IsNotNull(Descricao, "Descricao", "A descrição não pode ser nula")
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
