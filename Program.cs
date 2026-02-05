using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddProblemDetails();

// Pre-Populate DB with Genres on Startup
builder.AddGameStore();

var app = builder.Build();

// Enpoint Extensions
app.MapGamesEndpoint();
app.MapGenresEndpoint();

// DB Extensions
app.MigrateDb();

app.Run();
