using Templ.Infrastucture.Services;

namespace Templ.API.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IAuthService authService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            var user = authService.TryGetTokenUser(token);
            if (user != null)
            {
                context.Items["User"] = userService.FindByUserName(user);
            }
        }
        await _next(context);
    }
}
