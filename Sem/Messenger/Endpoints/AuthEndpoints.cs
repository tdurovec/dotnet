using Microsoft.AspNetCore.Identity;
using Messenger.Models;

namespace Messenger.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/account/logout", async (
            HttpContext httpContext,
            SignInManager<User> signInManager) =>
        {
            await signInManager.SignOutAsync();

            return Results.Redirect("/account/login");
        })
        .RequireAuthorization();

        return endpoints;
    }
}