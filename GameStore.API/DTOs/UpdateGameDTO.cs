using System.ComponentModel.DataAnnotations;

namespace GameStore.API.DTOs;

public record class UpdateGameDTO(
    [Required, StringLength(50)]
    string Name, 
    [Required]
    int GenreId, 
    [Range(1,100)]
    decimal Price, 
    DateOnly ReleaseDate,
    [Required, StringLength(500)]
    string Description,
    [Required]
    string FileName
);