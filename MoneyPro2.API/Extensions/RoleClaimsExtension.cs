using MoneyPro2.API.Models;
using System.Security.Claims;

namespace MoneyPro2.API.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.Email, user.Email.ToString())
        };
        //result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));
        return result;
    }
}
