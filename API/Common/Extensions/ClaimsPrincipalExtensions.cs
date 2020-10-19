using System.Security.Claims;
using Domain.Constants;

namespace API.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirstValue(CustomClaimTypes.Id));
        }
        public static string GetUserName(this ClaimsPrincipal user)
        {
            // claims is null, when http context doesn't exist.
            if (user == null)
            {
                return "system";
            }
            // claims doesn't contain UserName, when connection is anonymous
            return user.FindFirstValue(CustomClaimTypes.UserName) ?? "guest";
        }
    }
}
