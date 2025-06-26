using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
  const string GetGameEndpointName = "GetGame";

  // Extending The WebApplication Class
  public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
  {
    // Allows you to group resources together
    // will now put games in front of the query
    var group = app.MapGroup("games").WithParameterValidation();

    // GET /games
    // I think Include is like a join
    // .AsNoTracking tells EF Core to not keep track
    // Since we are not doing SaveChanges(). 
    // .ToListAsync returns a task instead of just
    group.MapGet("/", async (GameStoreContext dbContext) =>
      await dbContext.Games
        .Include(game => game.Genre)
        .Select(game => game.ToGameSummaryDto())
        .AsNoTracking()
        .ToListAsync());

    // GET /games/1
    group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
    {
      Game? game = await dbContext.Games.FindAsync(id);
      
      return game is null ?
        Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
    })
      .WithName(GetGameEndpointName);

    // POST /games
    group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
    {
      // ToEntity method is in Mapping
      Game game = newGame.ToEntity();

      dbContext.Games.Add(game);
      await dbContext.SaveChangesAsync();

      // Return the status of the POST
      // ToGameDetailsDto method is in mapping
      return Results.CreatedAtRoute(
        GetGameEndpointName,
        new { id = game.Id },
        game.ToGameDetailsDto());
    });

    // PUT /games/1
    group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
    {
      var existingGame = await dbContext.Games.FindAsync(id);

      if (existingGame is null)
      {
        return Results.NotFound();
      }

      dbContext.Entry(existingGame)
        .CurrentValues
        .SetValues(updatedGame.ToEntity(id));
      await dbContext.SaveChangesAsync();
      return Results.NoContent();
    });

    // DELETE /games/1
    group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
    {
      await dbContext.Games
        .Where(game => game.Id == id)
        .ExecuteDeleteAsync();

      return Results.NoContent();
    });

    return group;
  }
}


