using System;

namespace GameStore.Api.Models;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }

    // Creating Database Relationship
    public Genre? GenreDetails { get; set; }

    // For Lazy Loading
    public int GenreId { get; set; }
}
