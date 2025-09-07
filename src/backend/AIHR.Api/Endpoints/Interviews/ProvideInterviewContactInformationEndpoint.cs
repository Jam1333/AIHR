using AIHR.Api.Extensions;
using Application.Interviews.Commands.ProvideContactInformation;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Interviews;

public sealed class ProvideInterviewContactInformationEndpoint : ICarterModule
{
    private record ProvideInterviewContactInformationRequest(
        string Email,
        string PhoneNumer);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/interviews/{id:guid}/contact-information", 
            async (
                [FromRoute] Guid id, 
                [FromBody] ProvideInterviewContactInformationRequest request, 
                IMediator mediator) =>
            {
                var command = new ProvideInterviewContactInformationCommand(
                    id,
                    request.Email,
                    request.PhoneNumer);

                var result = await mediator.Send(command);

                return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
            })
            .AllowAnonymous();
    }
}
