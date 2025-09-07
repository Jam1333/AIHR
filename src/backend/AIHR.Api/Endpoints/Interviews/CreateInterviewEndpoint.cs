using AIHR.Api.Extensions;
using Application.Interviews.Commands.Create;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIHR.Api.Endpoints.Interviews;

public sealed class CreateInterviewEndpoint : ICarterModule
{
    private record CreateInterviewRequest(
        string Title,
        Dictionary<string, double> Weights,
        IFormFile ResumeFile,
        int MaxMessagesCount,
        Guid VacancyId);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/interviews", 
            [Authorize] 
            async (
                [FromForm] CreateInterviewRequest request, 
                ClaimsPrincipal user, 
                IMediator mediator) =>
            {
                Guid? currentUserId = user.GetCurrentUserId();

                if (currentUserId is null)
                {
                    return Results.Unauthorized();
                }

                var command = new CreateInterviewCommand(
                    request.Title,
                    request.Weights,
                    request.ResumeFile.ToFileRequest(),
                    request.MaxMessagesCount,
                    request.VacancyId,
                    currentUserId.Value);

                var result = await mediator.Send(command);

                return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
            })
            .DisableAntiforgery();
    }
}
