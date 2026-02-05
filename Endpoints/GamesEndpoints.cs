using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{

const string GetGameEndpointName = "GetGame";

    // private static readonly List<GameDto> games = [
    //     new (1, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
    //     new (2, "Tekken", "Fighting", 192.99M, new DateOnly(1992, 7, 15)),
    //     new (3, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
    //     new (4, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
    // ];

    // Extending Endpoints ------------------------------
    public static void MapGamesEndpoint(this WebApplication app)
    {    
        RouteGroupBuilder group = app.MapGroup("/games");

        // GET ------------------------------
        group.MapGet("/", async (GamesStoreContext context) => 
            await context.Games
                        .Include(game => game.GenreDetails)
                        .Select(game => new GameDto(
                            game.Id,
                            game.Name,
                            game.GenreDetails!.Name,
                            game.Price,
                            game.ReleaseDate
                        ))
                        .AsNoTracking()
                        .ToListAsync()
        );

        // GET By ID ------------------------------
        group.MapGet("/{id}", async (int id, GamesStoreContext context) => {

            var game = await context.Games
                .Include(g => g.GenreDetails)
                .FirstOrDefaultAsync(g => g.Id == id);

            // Does not handle Navigation Properties, returns null for GenreDetails
            // var game = await context.Games.FindAsync(id);

            if (game is null)
                return Results.NotFound();

            var gameDto = new GameDto(
                Id: game.Id,
                Name: game.Name,
                Genre: game.GenreDetails!.Name,
                Price: game.Price,
                ReleaseDate: game.ReleaseDate
            );

            // Causes logical error/bug when FindAsync is used.
            // var gameDto = new GameDto(
            //     Id: game.Id,
            //     Name: game.Name,
            //     Genre: game.GenreDetails?.Name ?? "Not Specified",
            //     Price: game.Price,
            //     ReleaseDate: game.ReleaseDate
            // );


            // return Results.Ok(gameDto);
            return game is null ? Results.NotFound() : Results.Ok(gameDto); 
            
        }).WithName(GetGameEndpointName);


        // POST ------------------------------
        group.MapPost("/", async (CreateGameDto request, GamesStoreContext context) =>
        {
            // Ceeate new object
            Game game = new(){  
                Name = request.Name,
                GenreId = request.GenreId,
                Price = request.Price,
                ReleaseDate = request.ReleaseDate
            };

            // Save to Context
            await context.Games.AddAsync(game);
            context.SaveChanges();
    
            // For Data ransfer to Client 
            GameDetailsDto gameDto = new(
                Id: game.Id,
                Name: game.Name,
                GenreId: game.GenreId,
                Price: game.Price,
                ReleaseDate: game.ReleaseDate
            );

            // Return Response
            return Results.CreatedAtRoute(GetGameEndpointName, new {id = gameDto.Id}, gameDto);
        });

        // PUT ------------------------------
        group.MapPut("/{id}", async (int id, UpdateGameDto request, GamesStoreContext context) =>
        {
            // Finds the first instance with matching id, does not handle duplicates
            var game = await context.Games.FindAsync(id);

            if (game is null) return Results.NotFound();

            // Update properties (Tracks Entity Changes and Commits on Saving)
            game.Name = request.Name;
            game.GenreId = request.GenreId;
            game.Price = request.Price;
            game.ReleaseDate = request.ReleaseDate;

            // Save to Context
            context.Games.Update(game);
            context.SaveChanges();

            return Results.Ok(new GameDetailsDto(
                Id: game.Id,
                Name: game.Name,
                GenreId: game.GenreId,
                Price: game.Price,
                ReleaseDate: game.ReleaseDate
            ));
            // return Results.NoContent();
        });


        // DELETE ------------------------------
        group.MapDelete("/{id}", async (int id, GamesStoreContext context) =>
        {
            var game = await context.Games.FindAsync(id);

            if (game is null) return Results.NotFound();    

            context.Games.Remove(game);
            context.SaveChanges();

            return Results.Ok(new GameDetailsDto(
                Id: game.Id,
                Name: game.Name,
                GenreId: game.GenreId,
                Price: game.Price,
                ReleaseDate: game.ReleaseDate
            ));
        });
    }
}
