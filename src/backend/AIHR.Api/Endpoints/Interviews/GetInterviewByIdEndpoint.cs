using AIHR.Api.Extensions;
using Application.Interviews.Queries.GetById;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Interviews;

public sealed class GetInterviewByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/interviews/{id:guid}", [Authorize] async ([FromRoute] Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetInterviewByIdQuery(id));

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
    }
}
