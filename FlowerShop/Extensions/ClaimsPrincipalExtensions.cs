using System.Security.Claims;

namespace FlowerShop.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user){
            var username = user.FindFirst(ClaimTypes.Name)?.Value;
            return username;
        }

        public static int GetUserId(this ClaimsPrincipal user){
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}