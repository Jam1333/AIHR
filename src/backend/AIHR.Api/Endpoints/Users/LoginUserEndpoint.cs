using AIHR.Api.Constants;
using AIHR.Api.Extensions;
using Application.Users.Commands.Login;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Users;

public sealed class LoginUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/auth/login", 
            async (HttpContext httpContext, [FromBody] LoginUserCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                if (result.IsFailed)
                {
                    return result.ToProblemDetails();
                }

                httpContext.Response.Cookies.Append(JwtConstants.JwtCookieKey, result.Value.Token);

                return Results.Ok();
            });
    }
}
