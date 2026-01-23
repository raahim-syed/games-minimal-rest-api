using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;      

var builder = WebApplication.CreateBuilder(args);

// Services
// builder.Services.AddValidation();

// Server Configuration
// HTTP Request Pipeline

var app = builder.Build();

app.MapGamesEndpoint();

app.Run();
