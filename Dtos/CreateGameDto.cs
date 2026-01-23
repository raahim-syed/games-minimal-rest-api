using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record CreateGameDto(
    [Required][StringLength(10)]string Name,
    [Range(1, 50)] int GenreId, 
    decimal Price,
    DateOnly ReleaseDate
);
