using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{

const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (1, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
        new (2, "Tekken", "Fighting", 192.99M, new DateOnly(1992, 7, 15)),
        new (3, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
        new (4, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
    ];

    // Extending Endpoints ------------------------------
    public static void MapGamesEndpoint(this WebApplication app)
    {
        
        RouteGroupBuilder group = app.MapGroup("/games");

        // GET ------------------------------
        group.MapGet("/games", () => games);

        group.MapGet("/{id}", () => (int id) => {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game); 
            
        }).WithName(GetGameEndpointName);


        // POST ------------------------------
        group.MapPost("/", (CreateGameDto request) =>
        {
            if(string.IsNullOrEmpty(request.Name)) return Results.BadRequest("Name is required!");

            // Ceeate new object
            GameDto game = new(
                Id: games.Count + 1,
                Name: request.Name,
                Genre: request.Genre,
                Price: request.Price,
                ReleaseDate: request.ReleaseDate
            );

            // Add to list
            games.Append(game);

            // Return Response
            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
        });

        // PUT ------------------------------
        group.MapPut("/{id}", (int id, UpdateGameDto request) =>
        {
            int index = games.FindIndex((game) => game.Id == id);

            if(index == -1) return Results.NotFound();

            games[index] = new GameDto(    
                Id: id,
                Name: request.Name,
                Genre: request.Genre,
                Price: request.Price,
                ReleaseDate: request.ReleaseDate
            );

            return Results.NoContent();
        });


        // DELETE ------------------------------
        group.MapDelete("/{id}", (int id) =>
        {
            int index = games.FindIndex((game) => game.Id == id);

            if(index == -1) return Results.NotFound();

            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });
    }

}
