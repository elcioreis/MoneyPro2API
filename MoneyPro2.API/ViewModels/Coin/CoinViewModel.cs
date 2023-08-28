using MoneyPro2.Shared.ViewModels;

namespace MoneyPro2.API.ViewModels.Coin;

public class CoinViewModel : ViewModel
{
    public string? Apelido { get; set; }
    public string? Simbolo { get; set; }
    //public string? Padrao { get; set; }
    public string? MoedaVirtual { get; set; }
    public string? BancoCentral { get; set; }
    public string? Eletronica { get; set; }
    public string? Observacao { get; set; }
}
