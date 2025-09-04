using Application.Vacancies.Queries.GetAll;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Vacancies;

public sealed class GetAllVacanciesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/vacancies", 
            [Authorize] async ([FromQuery] Guid? userId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllVacanciesQuery(userId));

                return Results.Ok(result);
            });
    }
}
