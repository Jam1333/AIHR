using System.Security.Claims;

namespace AIHR.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetCurrentUserId(this ClaimsPrincipal user)
    {
        string? nameIdentifier = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (nameIdentifier is null || !Guid.TryParse(nameIdentifier, out Guid userId))
        {
            return null;
        }

        return userId;
    }
}
