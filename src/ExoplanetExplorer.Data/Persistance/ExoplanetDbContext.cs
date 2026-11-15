using Microsoft.EntityFrameworkCore;

public sealed class ExoplanetDbContext : DbContext
{
    public ExoplanetDbContext(DbContextOptions<ExoplanetDbContext> options)
        : base(options) { }

    public DbSet<Exoplanet> Exoplanets => Set<Exoplanet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exoplanet>(entity =>
        {
           entity.HasKey(planet => planet.Id);
           entity.HasIndex(planet => planet.Name).IsUnique();

           entity.Property(planet => planet.Name).IsRequired();
           entity.Property(planet => planet.Hostname).IsRequired();
        });
    }
}