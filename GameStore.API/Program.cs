using GameStore.API;
using GameStore.API.Data;
using GameStore.API.EndPoints;
using GameStore.API.Services;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlite(connString));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<GameStoreContext>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();

builder.Services.AddTransient<IEmailSender, NoOpEmailSender>();
var app = builder.Build();
app.MapIdentityApi<ApplicationUser>();
app.MapGamesEndpoints();
app.MapGenresEndpoints();
app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager,
    [FromBody] object empty) =>
{
    if (empty != null)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
    return Results.Unauthorized();
})
.WithOpenApi()
.RequireAuthorization();
await app.MigrateDbAsync();

app.Run();
