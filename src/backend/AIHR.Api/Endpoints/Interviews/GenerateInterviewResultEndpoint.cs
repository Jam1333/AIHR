using AIHR.Api.Extensions;
using Application.Interviews.Commands.GenerateResult;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIHR.Api.Endpoints.Interviews;

public sealed class GenerateInterviewResultEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/interviews/{id:guid}/generate-result", [Authorize] async ([FromRoute] Guid id, ClaimsPrincipal user, IMediator mediator) =>
        {
            Guid? currentUserId = user.GetCurrentUserId();

            if (currentUserId is null)
            {
                return Results.Unauthorized();
            }

            var command = new GenerateInterviewResultCommand(
                id, 
                currentUserId.Value);

            var result = await mediator.Send(command);

            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        });
    }
}
