using DVLD_Buisness;

namespace DVLD.Token
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
   
            if (context.Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                var token = tokenHeader.ToString().Replace("Bearer ", "");

              
                if (await clsBlackList.IsTokenExistAsync(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token has been blacklisted.");
                    return;
                }
            }

            await _next(context); 
        }
    }

}
