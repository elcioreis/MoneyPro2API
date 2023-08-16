using MoneyPro2.Shared.ViewModels;

namespace MoneyPro2.API.ViewModels.Institution;

public class InstitutionViewModel : ViewModel
{
    public int TipoInstituicaoId { get; set; }
    public string Apelido { get; set; } = null!;
    public string Descricao { get; set; } = null!;
}
