using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{

const string GetGenreEndpointName = "GetGenre";

    // Extending Endpoints ------------------------------
    public static void MapGenresEndpoint(this WebApplication app)
    {
        
        RouteGroupBuilder group = app.MapGroup("/genres");

        // GET ------------------------------
        group.MapGet("/", async (GamesStoreContext context) => 
            await context.Genres
                        .Select(genre => new GenreDto(
                            genre.Id,
                            genre.Name
                        ))
                        .AsNoTracking()
                        .ToListAsync()
        );

        // GET By ID ------------------------------
        group.MapGet("/{id}", async (int id, GamesStoreContext context) => {

            var genre =  await context.Genres.FindAsync(id);

            if (genre is null)
                return Results.NotFound();

            var genreDto = new GenreDto(
                Id: genre.Id,
                Name: genre.Name
            );

            return Results.Ok(genreDto);
        }).WithName(GetGenreEndpointName);

        // POST ------------------------------
        group.MapPost("/", async (GenreDto request, GamesStoreContext context) =>
        {
            // Ceeate new object
            Genre genre = new(){
                Name = request.Name
            };

            // Add to context
            context.Genres.Add(genre);
            await  context.SaveChangesAsync();

            // Return the created genre
            var genreDto = new GenreDto(
                Id: genre.Id,
                Name: genre.Name
            );

            return Results.CreatedAtRoute(
                GetGenreEndpointName,
                new { id = genreDto.Id },
                genreDto
            );
        }).WithName(GetGenreEndpointName);

        // PUT ------------------------------
        group.MapPut("/{id}", async (int id, GenreDto request, GamesStoreContext context) =>
        {
            var genre = await context.Genres.FindAsync(id);

            if (genre is null) return Results.NotFound();

            // Update properties
            genre.Name = request.Name;

            // Save changes
            await context.SaveChangesAsync();

            return Results.NoContent();
        });


        //  DELETE ------------------------------
        group.MapDelete("/{id}", async (int id, GamesStoreContext context) =>
        {
            var genre = await context.Genres.FindAsync(id);

            if (genre is null) return Results.NotFound();

            context.Genres.Remove(genre);
            await context.SaveChangesAsync();

            return Results.NoContent();
        }); 
    }
}