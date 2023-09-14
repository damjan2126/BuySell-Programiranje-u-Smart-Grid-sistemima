using BuySell.Contracts.Exceptions;
using BuySell.Data.Enums;
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
            var userRole = user.Claims.Where(c => c.Type == ClaimTypeEnum.Roles.ToString()).Select(x => x.Value).ToList() ?? 
                throw new InvalidTokenException("Unauthorized token");
            

            return (List<string>)userRole;
        }
    }
}
