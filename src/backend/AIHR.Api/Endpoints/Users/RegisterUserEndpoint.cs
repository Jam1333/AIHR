using AIHR.Api.Constants;
using AIHR.Api.Extensions;
using Application.Users.Commands.Register;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Users;

public sealed class RegisterUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async ([FromBody] RegisterUserCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
    }
}
