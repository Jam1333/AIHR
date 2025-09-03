using AIHR.Api.Extensions;
using Application.Users.Commands.Update;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIHR.Api.Endpoints.Users;

public sealed class UpdateUserEndpoint : ICarterModule
{
    private record UpdateUserRequest(string Username, string Email);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "/users/{id:guid}",
            [Authorize]
            async (
                [FromRoute] Guid id, 
                [FromBody] UpdateUserRequest request, 
                ClaimsPrincipal user, 
                IMediator mediator) =>
            {
                Guid? currentUserId = user.GetCurrentUserId();

                if (currentUserId is null)
                {
                    return Results.Unauthorized();
                }

                var result = await mediator.Send(
                    new UpdateUserCommand(
                        id,
                        request.Username,
                        request.Email,
                        currentUserId.Value));

                return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
            });
    }
}
