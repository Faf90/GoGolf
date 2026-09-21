using Microsoft.EntityFrameworkCore;
using GoGolf.Api.Features.Clubs;
using GoGolf.Api.Data;

var builder = WebApplication.CreateBuilder(args);

const string DevCorsPolicy = "DevCors";

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy(DevCorsPolicy, policy =>
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.UseCors(DevCorsPolicy);

app.MapGet("/api/health", () => new
{
    status = "healthy",
    service = "GoGolf.Api",
    timestamp = DateTime.UtcNow
});

app.MapClubEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();