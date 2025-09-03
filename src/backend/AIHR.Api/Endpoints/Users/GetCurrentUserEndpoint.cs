using AIHR.Api.Extensions;
using Application.Users.Queries.GetById;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AIHR.Api.Endpoints.Users;

public sealed class GetCurrentUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/users/current", 
            [Authorize] async (ClaimsPrincipal user, IMediator mediator) =>
            {
                Guid? currentUserId = user.GetCurrentUserId();

                if (currentUserId is null)
                {
                    return Results.Unauthorized();
                }

                var result = await mediator.Send(new GetUserByIdQuery(currentUserId.Value));

                return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
            });
    }
}
