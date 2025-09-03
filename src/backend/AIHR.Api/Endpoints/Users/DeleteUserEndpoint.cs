using AIHR.Api.Constants;
using AIHR.Api.Extensions;
using Application.Users.Commands.Delete;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIHR.Api.Endpoints.Users;

public sealed class DeleteUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/users/{id:guid}", 
            [Authorize] 
            async (
                HttpContext context,
                [FromRoute] Guid id, 
                ClaimsPrincipal user, 
                IMediator mediator) =>
            {
                Guid? currentUserId = user.GetCurrentUserId();

                if (currentUserId is null)
                {
                    return Results.Unauthorized();
                }

                var result = await mediator.Send(new DeleteUserCommand(id, currentUserId.Value));

                if (result.IsFailed)
                {
                    return result.ToProblemDetails();
                }

                context.Response.Cookies.Delete(JwtConstants.JwtCookieKey);

                return Results.NoContent();
            });
    }
}
