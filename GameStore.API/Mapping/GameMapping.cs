using GameStore.API.DTOs;
using GameStore.API.Entities;

namespace GameStore.API.Mapping;

public static class GameMapping
{   
    //Convert GameDTO to GameEntity
    public static Game ToGameEntity(this CreateGameDTO game){
        return new Game(){
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
            Description = game.Description,
            Filename = game.FileName
        }; 
    }
    //Convert GameEntity to GameSummaryDTO
    public static GameSummaryDTO ToGameSummaryDTO(this Game game){
        return new (
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate,
            game.Description!,
            game.Filename!
        );
    }
    //Convert GameEntity to GameDetailsDTO
     public static GameDetailsDTO ToGameDetailsDTO(this Game game){
        return new (
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate,
            game.Description!,
            game.Filename!
        );
    }

     public static Game ToGameEntity(this UpdateGameDTO game, int id){
        return new Game(){
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
            Description = game.Description,
            Filename = game.FileName

        }; 
    }
}
