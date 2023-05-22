using System.Security.Claims;

namespace FlowerShop.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user){
            var test = user.FindFirst(ClaimTypes.Name)?.Value;
            Console.WriteLine();
            Console.WriteLine(ClaimTypes.Name);
            return test;
        }

        public static int GetUserId(this ClaimsPrincipal user){
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}