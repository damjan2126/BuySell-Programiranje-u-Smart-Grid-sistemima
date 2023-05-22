using BuyAndSell.Contracts.Exceptions;
using BuyAndSell.Data.Enums;
using System.Security.Claims;
using System.Text.Json;

namespace BuySell.Host.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        internal static long GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypeEnum.Id.ToString()) ?? 
                throw new InvalidTokenException("Unauthorized token");
            
            return Convert.ToInt64(userId.Value);
        }

        internal static List<string> GetRoles(this ClaimsPrincipal user)
        {
            var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypeEnum.Roles.ToString()) ?? 
                throw new InvalidTokenException("Unauthorized token");
            
            var roles = JsonSerializer.Deserialize<List<string>>(userRole.Value) ?? new();

            return roles;
        }
    }
}
