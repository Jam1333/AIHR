using Application.Analyses.Queries.GetAll;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Analyses;

public sealed class GetAllAnalysesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/analyses", 
            [Authorize] async ([FromQuery] Guid? vacancyId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllAnalysesQuery(vacancyId));

                return Results.Ok(result);
            });
    }
}
