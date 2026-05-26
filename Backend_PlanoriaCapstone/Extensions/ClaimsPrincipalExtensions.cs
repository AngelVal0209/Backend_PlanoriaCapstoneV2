using System.Security.Claims;

namespace Backend_PlanoriaCapstone.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? ObtenerUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claim, out var id) ? id : null;
        }
    }
}
