using AIHR.Api.Extensions;
using Application.Users.Queries.GetById;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Users;

public sealed class GetUserByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:guid}", async ([FromRoute] Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
    }
}
