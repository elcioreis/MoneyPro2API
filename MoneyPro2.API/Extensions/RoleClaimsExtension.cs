using MoneyPro2.API.Models;
using System.Security.Claims;

namespace MoneyPro2.API.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new Claim(ClaimTypes.PrimarySid, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.Email, user.Email.ToString())
        };
        return result;
    }

    public static string GetUserName(this ClaimsPrincipal user)
    {
        if (user != null)
        {
            return user.Identity!.Name ?? string.Empty;
        }

        return string.Empty;
    }

    public static int GetUserId(this ClaimsPrincipal user)
    {

        const string _primarysid = "primarysid";
        if (user != null && user.HasClaim(c => c.Type.ToLower().EndsWith(_primarysid)))
        {
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            string strID =
                user.Claims
                    .FirstOrDefault(x => x.Type.ToLowerInvariant().EndsWith(_primarysid))
                    .Value ?? string.Empty;
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

            int id = 0;

            if (int.TryParse(strID, out id))
            {
                return id;
            }
        }

        return 0;
    }

    public static string GetUserEmail(this ClaimsPrincipal user)
    {
        const string _emailaddress = "emailaddress";
        if (user != null && user.HasClaim(c => c.Type.ToLower().EndsWith(_emailaddress)))
        {
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            return user.Claims
                    .FirstOrDefault(x => x.Type.ToLowerInvariant().EndsWith(_emailaddress))
                    .Value ?? string.Empty;
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
        }

        return string.Empty;
    }
}
