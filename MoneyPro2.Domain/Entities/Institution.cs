using Flunt.Notifications;
using Flunt.Validations;

namespace MoneyPro2.Domain.Entities;

public class Institution : Notifiable<Notification>
{
    public Institution() { }
    public Institution(int userId, string? tipoInstituicaoId, string? apelido, string? descricao)
    {
        InstituicaoId = 0;
        UserId = userId;
        SetTipoInsituicao(tipoInstituicaoId);
        SetApelido(apelido);
        SetDescricao(descricao);
        InstitutionContracts();
    }

    public int InstituicaoId { get; private set; }
    public int UserId { get; private set; }
    public int? TipoInstituicaoId { get; private set; }
    public string? Apelido { get; private set; } = null!;
    public string? Descricao { get; private set; } = null!;
    public bool? Ativo { get; private set; } = true;
    public User User { get; set; } = null!;
    public InstitutionType InstitutionType { get; set; } = null!;

    public void SetTipoInsituicao(string? tipoInstituicaoId)
    {
        if (int.TryParse(tipoInstituicaoId, out int idTipo))
            TipoInstituicaoId = idTipo;
        else
            TipoInstituicaoId = null;

        InstitutionContracts();
    }

    public void SetApelido(string? apelido)
    {
        Apelido = apelido;
        InstitutionContracts();
    }

    public void SetDescricao(string? descricao)
    {
        Descricao = descricao;
        InstitutionContracts();
    }

    public void SetInactive()
    {
        Ativo = false;
        InstitutionContracts();
    }

    public void SetActive()
    {
        Ativo = true;
        InstitutionContracts();
    }

    private void InstitutionContracts()
    {
        Clear();
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNull(TipoInstituicaoId, "TipoInstituicaoID", "O tipo de instituição não pode ser nulo")
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
