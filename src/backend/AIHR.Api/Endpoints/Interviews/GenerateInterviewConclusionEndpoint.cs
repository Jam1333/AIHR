using AIHR.Api.Extensions;
using Application.Interviews.Commands.GenerateConclusion;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Interviews;

public sealed class GenerateInterviewConclusionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/interviews/{id:guid}/generate-conclusion", 
            async ([FromRoute] Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GenerateInterviewConclusionCommand(id));

                return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
            })
            .AllowAnonymous();
    }
}
