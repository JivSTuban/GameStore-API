using GameStore.API.Data;
using GameStore.API.DTOs;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.EndPoints;
//Static class with Extension Methods
public static class GamesEndpoints
{
const string GetGameEndpointName = "GetGame";
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){
        var group = app.MapGroup("games").WithParameterValidation();
        // GET games list from /games
        group.MapGet("/", async (GameStoreContext gameStoreContext)=> await gameStoreContext.Games.Include(game => game.Genre).Select(game => game.ToGameSummaryDTO()).AsNoTracking().ToListAsync());

        // GET individual game from /games/id
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext)=> {
            Game? game = await dbContext.Games.FindAsync(id);
            
            return game is null ? Results.NotFound(): Results.Ok(game.ToGameDetailsDTO());
            })
            .WithName(GetGameEndpointName)
            .RequireAuthorization();
            

        // POST/create new game
        group.MapPost("/", async (CreateGameDTO newGame, GameStoreContext dbContext)=> {
            //  Converts the Game DTO into a Game Entity
            Game game = newGame.ToGameEntity();
            //  Saves converted Game Entity to database
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            /*  The created game is converted back into a Entity 
                to render Genre Name by the use of the GenreID in the Game Entity.
                The GetGameEndpointName concatenates the id the current group search.  */
            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game.ToGameDetailsDTO());
        })
        .RequireAuthorization();


        group.MapPut("/{id}", async (int id, UpdateGameDTO updatedGameDTO, GameStoreContext dbContext) => {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null) {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(
                updatedGameDTO.ToGameEntity(id)
            );

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .RequireAuthorization();

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext)=>{
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        })
        .RequireAuthorization();
        return group;
    }
}
