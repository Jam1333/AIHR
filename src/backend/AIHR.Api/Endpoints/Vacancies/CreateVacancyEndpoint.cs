using Application.Vacancies.Commands.Create;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AIHR.Api.Extensions;

namespace AIHR.Api.Endpoints.Vacancies;

public sealed class CreateVacancyEndpoint : ICarterModule
{
    private record CreateVacancyRequest(
        string Title, 
        string Language, 
        string[] Categories, 
        IFormFile File);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/vacancies", 
            [Authorize] 
            async (
                [FromForm] CreateVacancyRequest request, 
                ClaimsPrincipal user, 
                IMediator mediator) =>
            {
                Guid? currentUserId = user.GetCurrentUserId();

                if (currentUserId is null)
                {
                    return Results.Unauthorized();
                }

                var command = new CreateVacancyCommand(
                    request.Title,
                    request.Language,
                    request.Categories,
                    request.File.ToFileRequest(),
                    currentUserId.Value);

                var result = await mediator.Send(command);

                return result.IsSuccess ? Results.Accepted(result.Value.ToString()) : result.ToProblemDetails();
            })
            .DisableAntiforgery();
    }
}
