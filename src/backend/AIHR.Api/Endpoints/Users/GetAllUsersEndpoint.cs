using Application.Users.Queries.GetAll;
using Carter;
using Mediator;

namespace AIHR.Api.Endpoints.Users;

public sealed class GetAllUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (IMediator mediator) =>
        {
            var result = await mediator.Send(GetAllUsersQuery.Instance);

            return Results.Ok(result);
        });
    }
}
