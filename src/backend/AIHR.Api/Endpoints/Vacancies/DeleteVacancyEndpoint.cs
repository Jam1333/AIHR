using AIHR.Api.Extensions;
using Application.Vacancies.Commands.Delete;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIHR.Api.Endpoints.Vacancies;

public sealed class DeleteVacancyEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/vacancies/{id:guid}", 
            [Authorize] 
            async (
                [FromRoute] Guid id, 
                ClaimsPrincipal user,
                IMediator mediator) =>
            {
                Guid? currentUserId = user.GetCurrentUserId();

                if (currentUserId is null)
                {
                    return Results.Unauthorized();
                }

                var command = new DeleteVacancyCommand(
                    id, 
                    currentUserId.Value);

                var result = await mediator.Send(command);

                return result.IsSuccess ? Results.NoContent() : result.ToProblemDetails();
            });
    }
}
