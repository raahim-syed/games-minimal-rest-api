using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    //Creates DB on Startup of application if it doesn't exist and applies any pending migrations
    public static void MigrateDb(this WebApplication app)
    {
        // Creating Scope
        using var scope = app.Services.CreateScope( );
        var dbContext = scope.ServiceProvider
                            .GetRequiredService<GamesStoreContext>();

        dbContext.Database.Migrate();
    }

    public static void AddGameStore(this WebApplicationBuilder builder)
    {
        string connString = builder.Configuration.GetConnectionString("GameStore") ?? "Data Source=GameStore.db"; 
        builder.Services.AddSqlite<GamesStoreContext>(
            connString, 
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                // Pre Populate Genres
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Action" },
                        new Genre { Name = "Adventure" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Strategy" }
                    );

                    context.SaveChanges();
                }
            }));

    }
}
