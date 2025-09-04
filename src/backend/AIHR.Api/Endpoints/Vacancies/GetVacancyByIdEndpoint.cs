using AIHR.Api.Extensions;
using Application.Vacancies.Queries.GetById;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Vacancies;

public sealed class GetVacancyByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/vacancies/{id:guid}", 
            [Authorize] async ([FromRoute] Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetVacancyByIdQuery(id));

                return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
            });
    }
}
