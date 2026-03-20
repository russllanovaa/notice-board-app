using NoticeBoardAPI.Interfaces;

namespace NoticeBoardAPI.Middleware
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        public TokenBlacklistMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authorizationService)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()?.Split(" ").Last();

            if(token != null && authorizationService.IsTokenBlacklisted(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Токен недійсний");
                return;
            }
            await requestDelegate(context);
        }
    }
}
