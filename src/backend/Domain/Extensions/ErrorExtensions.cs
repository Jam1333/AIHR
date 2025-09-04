using FluentResults;

namespace Domain.Extensions;

public static class ErrorExtensions
{
    public static ApplicationException ToApplicationException(this IError error)
    {
        return new ApplicationException(error.Message);
    }
}
