namespace MoneyPro2.API.ViewModels.Status;

public class StatusViewModel
{
    public StatusViewModel(string status)
    {
        Status = status;
    }

    public string Status { get; set; } = string.Empty;
}

