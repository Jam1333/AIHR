using AIHR.Api.Extensions;
using Application.Analyses.Queries.GetById;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Analyses;

public sealed class GetAnalysisByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/analyses/{id:guid}", 
            [Authorize] async ([FromRoute] Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAnalysisByIdQuery(id));

                return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
            });
    }
}
