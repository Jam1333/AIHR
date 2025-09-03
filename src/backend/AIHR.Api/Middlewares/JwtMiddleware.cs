using AIHR.Api.Constants;

namespace AIHR.Api.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate next;

    public JwtMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue(JwtConstants.JwtCookieKey, out string? token) && token is not null)
        {
            context.Request.Headers.Authorization = $"Bearer {token}";
        }

        await next.Invoke(context);
    }
}
