namespace MoneyPro2.API.Models;

public class UserLogin
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public DateTime LoginTime { get; private set; }
    public User Users { get; private set; } = null!;
}
