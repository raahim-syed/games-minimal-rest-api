using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddProblemDetails();

// Registery Entity Framework

builder.AddGameStore();

// Server Configuration
// HTTP Request Pipeline

var app = builder.Build();

app.MapGamesEndpoint();

app.MigrateDb();

app.Run();
