using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GamesStoreContext : DbContext
{
    public GamesStoreContext(DbContextOptions options) : base (options){}

    public DbSet<Game> Games {get; set;}
    public DbSet<Genre> Genres {get; set;}
}
