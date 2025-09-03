using AIHR.Api.Constants;
using Carter;

namespace AIHR.Api.Endpoints.Users;

public sealed class LogoutUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/logout", (HttpContext httpContext) =>
        {
            httpContext.Response.Cookies.Delete(JwtConstants.JwtCookieKey);

            return Results.Ok();
        });
    }
}
