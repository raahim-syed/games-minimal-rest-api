using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddProblemDetails();

builder.AddGameStore();

// Server Configuration
// HTTP Request Pipeline

var app = builder.Build();

// Enpoint Extensions
app.MapGamesEndpoint();
app.MapGenresEndpoint();

// DB Extensions
app.MigrateDb();

app.Run();
