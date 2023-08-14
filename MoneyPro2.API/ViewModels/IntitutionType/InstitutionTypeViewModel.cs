using MoneyPro2.Shared.ViewModels;

namespace MoneyPro2.API.ViewModels.IntitutionType;

public class InstitutionTypeViewModel : ViewModel
{
    //public int TipoInsituicaoId { get; set; }
    public string? Apelido { get; set; }
    public string? Descricao { get; set; }
    public bool Ativo { get; set; }
}
