// using GameStore.Api.Dtos;

// const string GetGameEndpointName = "GetGame";


// var builder = WebApplication.CreateBuilder(args);

// // Server Configuration

// List<GameDto> games = [
//     new (1, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
//     new (1, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
//     new (1, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
//     new (1, "Street Fighter 2", "Fighting", 199.99M, new DateOnly(1992, 7, 15)),
// ];

// // HTTP Request Pipeline

// var app = builder.Build();

// app.MapGet("/games", () => games);

// app.MapGet("/games/{id}", () => (int id) => {
//     var game = games.Find(game => game.Id == id);

//     return game is null ? Results.NotFound() : Results.Ok(game); 
    
// }).WithName(GetGameEndpointName);

// var connString = "Data Source=GameStore.db";
// builder.Services.AddSqlite<GameStoreContext>(connString);

// app.MapPost("/games", (CreateGameDto request) =>
// {
//     // Ceeate new object
//     GameDto game = new(
//         Id: games.Count + 1,
//         Name: request.Name,
//         Genre: request.Genre,
//         Price: request.Price,
//         ReleaseDate: request.ReleaseDate
//     );

//     // Add to list
//     games.Append(game);

//     // Return Response
//     return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
// });


// app.MapPut("/games/{id}", (int id, UpdateGameDto request) =>
// {
//     int index = games.FindIndex((game) => game.Id == id);

//     if(index == -1) return Results.NotFound();

//     games[index] = new GameDto(    
//         Id: id,
//         Name: request.Name,
//         Genre: request.Genre,
//         Price: request.Price,
//         ReleaseDate: request.ReleaseDate
//     );

//     return Results.NoContent();
// });

// app.MapDelete("/games/{id}", (int id) =>
// {
//     int index = games.FindIndex((game) => game.Id == id);

//     if(index == -1) return Results.NotFound();

//     games.RemoveAll(game => game.Id == id);

//     return Results.NoContent();
// });

// app.Run();
