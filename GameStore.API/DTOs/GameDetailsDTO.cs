using GameStore.API.Entities;

namespace GameStore.API.DTOs;

public record class GameDetailsDTO(int Id, string Name, int GenreID, decimal Price, DateOnly ReleaseDate, string Description, string FileName);
