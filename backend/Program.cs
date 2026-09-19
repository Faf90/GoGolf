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

var app = builder.Build();

app.UseCors(DevCorsPolicy);

app.MapGet("/api/health", () => new
{
    status = "healthy",
    service = "GoGolf.Api",
    timestamp = DateTime.UtcNow
});

app.Run();