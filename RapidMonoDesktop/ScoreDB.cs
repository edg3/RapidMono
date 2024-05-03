using Microsoft.EntityFrameworkCore;
using RapidMonoDesktop;

namespace RapidMono;

public class ScoreDB : DbContext
{
    public static ScoreDB I { get; private set; }
    public ScoreDB()
    {
        if (I is not null) throw new System.Exception("ScoreDB already exists");
        I = this;
    }

    private const string ConnectionString = "Data Source=ScoreDB.db";
    public DbSet<ScoreItem> ScoreItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ScoreItem>().ToTable("ScoreItems");
    }
}
