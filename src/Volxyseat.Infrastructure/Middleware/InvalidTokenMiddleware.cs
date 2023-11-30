using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Volxyseat.Domain.Models.AuthModel;
using System.Collections.Generic;

public class InvalidTokenMiddleware
{
    private readonly RequestDelegate _next;
    private List<InvalidToken> _invalidTokens;

    public InvalidTokenMiddleware(RequestDelegate next)
    {
        _next = next;
        _invalidTokens = new List<InvalidToken>();
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (_invalidTokens.Any(t => t.TokenId == token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(context);
    }
}