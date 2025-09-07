using AIHR.Api.Extensions;
using Application.Analyses.Commands.Create;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace AIHR.Api.Endpoints.Analyses;

public sealed class CreateAnalysisEndpoint : ICarterModule
{
    private record CreateAnalysisRequest(
        string Title,
        Dictionary<string, double> Weights,
        Guid VacancyId,
        IFormFile[] Files);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/analyses", 
            [Authorize] 
            async (
                [FromForm] CreateAnalysisRequest request, 
                ClaimsPrincipal user, 
                IMediator mediator) =>
            {
                Guid? currentUserId = user.GetCurrentUserId();

                if (currentUserId is null)
                {
                    return Results.Unauthorized();
                }

                var command = new CreateAnalysisCommand(
                    request.Title, 
                    request.Weights, 
                    request.Files.Select(f => f.ToFileRequest()),
                    request.VacancyId, 
                    currentUserId.Value);

                var result = await mediator.Send(command);

                return result.IsSuccess ? Results.Accepted(result.Value.ToString()) : result.ToProblemDetails();
            })
            .DisableAntiforgery();
    }
}
