using Flunt.Notifications;
using Flunt.Validations;
using MoneyPro2.Domain.Functions;
using MoneyPro2.Domain.ValueObjects;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MoneyPro2.Domain.Entities;

public partial class User : Notifiable<Notification>
{
    private readonly Regex _allowedChars = AllowedChars();
    private readonly Regex _strongPassword = StrongPassword();

    public User() { }

    public User(string username, string nome, string email, string cpf, string senha)
    {
        UserId = 0;
        if (!string.IsNullOrEmpty(username))
            Username = username.Trim().ToLower();
        if (!string.IsNullOrEmpty(nome))
            Nome = nome;
        if (!string.IsNullOrEmpty(email))
            Email = new Email(email);
        if (!string.IsNullOrEmpty(cpf))
            CPF = new CPF(cpf);
        if (!string.IsNullOrEmpty(senha))
            Senha = senha;
        Criptografada = Tools.GenerateMD5(Username, Senha);

        UserContracts();
    }


    public int UserId { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string Nome { get; private set; } = string.Empty;
    public Email Email { get; private set; } = new("");
    public bool EmailVerificado { get; private set; } = false;
    public CPF CPF { get; private set; } = new("");
    [JsonIgnore]
    public string Senha { get; private set; } = string.Empty;
    [JsonIgnore]
    public string Criptografada { get; set; } = string.Empty;
    public ICollection<UserLogin> UserLogins { get; private set; } = new List<UserLogin>();
    public ICollection<InstitutionType> InstitutionTypes { get; private set; } = new List<InstitutionType>();
    public ICollection<Institution> Institutions { get; private set; } = new List<Institution>();

    private void UserContracts()
    {
        Clear();
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsTrue(
                    Username?.Length >= 1 && Username?.Length <= 20,
                    "Username",
                    "O username deve ter entre 1 e 20 caracteres"
                )
                .IsTrue(
                    //!string.IsNullOrEmpty(Username) &&
                    _allowedChars.IsMatch(Username ?? ""),
                    "Username",
                    "O username só pode ter letras, números, arroba ou ponto"
                )
                .IsTrue(
                    Nome?.Length >= 2 && Nome?.Length <= 50,
                    "Nome",
                    "O nome deve ter entre 2 e 50 caracteres"
                )
                .IsTrue(Senha?.Length >= 8, "Senha", "A senha deve ter ao menos oito caracteres")
                .IsTrue(
                    _strongPassword.IsMatch(Senha ?? ""),
                    "Senha",
                    "A senha deve ter minúsculas, maiúsculas, números e caracteres especiais"
                )
        );
        AddNotifications(Email?.Notifications);
        AddNotifications(CPF?.Notifications);
    }

    [GeneratedRegex("^([a-z0-9@.]){1,20}$")]
    private static partial Regex AllowedChars();

    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    private static partial Regex StrongPassword();
}
