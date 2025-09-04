using Domain.Constants;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Domain.Extensions;

namespace AIHR.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this ResultBase result)
    {
        IError error = result.GetError();

        int statusCode = GetStatusCode(error);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Type = GetType(statusCode),
            Title = GetTitle(statusCode),
            Detail = error.Message,
        };

        if (error.Metadata.TryGetValue(ErrorConstants.ErrorsMetadataKey, out object? errors) && errors is not null)
        {
            problemDetails.Extensions.Add(ErrorConstants.ErrorsMetadataKey, errors);
        }

        return Results.Problem(problemDetails);

        static int GetStatusCode(IError error) => (error.Metadata.GetValueOrDefault(ErrorConstants.StatusMetadataKey) as int?) ?? 500;

        static string GetTitle(int statusCode) =>
          statusCode switch
          {
              400 => "Bad Request",
              401 => "Unauthorized",
              403 => "Access Denied",
              404 => "Not Found",
              409 => "Conflict",
              _ => "Server Failure",
          };

        static string GetType(int statusCode) =>
          statusCode switch
          {
              400 => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
              401 => "https://tools.ietf.org/html/rfc7235#section-3.1",
              403 => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
              404 => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
              409 => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
              _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
          };
    }
}

