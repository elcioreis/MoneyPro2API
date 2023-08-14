namespace MoneyPro2.API.Models;

public class UserLogin
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime LoginTime { get; set; }
    public User User { get; set; } = null!;
}
