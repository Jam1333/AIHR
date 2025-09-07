using Application.Interviews.Queries.GetAll;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Interviews;

public sealed class GetAllInterviewsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/interviews", [Authorize] async ([FromQuery] Guid? vacancyId, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllInterviewsQuery(vacancyId));

            return Results.Ok(result);
        });
    }
}
