using Microsoft.EntityFrameworkCore;
using GoGolf.Api.Data;

namespace GoGolf.Api.Features.Clubs;

public static class ClubEndpoints
{
    public static RouteGroupBuilder MapClubEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clubs");

        group.MapGet("/", async (AppDbContext dbContext) =>
            await dbContext.Clubs
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new ClubSummaryDto(c.Id, c.Name, c.Address, c.FromPrice, c.Rating, c.Bays.Count(b => b.IsActive)))
                .ToListAsync());
        
        group.MapGet("/{id:int}", async (int id, AppDbContext dbContext) =>
        {
            var club = await dbContext.Clubs
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new ClubDetailDto(c.Id, c.Name, c.Address, c.FromPrice, c.Rating, 
                    c.Bays
                    .Where(b => b.IsActive)
                    .OrderBy(b => b.Name)
                    .Select(b => new BayDto(b.Id, b.Name, b.IsActive))
                    .ToList()))
                .FirstOrDefaultAsync();

            return club is null
                ? Results.NotFound()
                : Results.Ok(club);
        });

        return group;
    }
}
