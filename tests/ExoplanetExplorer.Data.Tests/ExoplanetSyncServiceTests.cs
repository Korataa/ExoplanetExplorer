using System.Net;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ExoplanetExplorer.Data.Tests.NASA;

public class ExoplanetSyncServiceTests
{
    [Fact]
    public async Task SyncUpdatesExistingPlanetAndPreservesId()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<ExoplanetDbContext>()
            .UseSqlite(connection)
            .Options;
        await using var db = new ExoplanetDbContext(options);
        await db.Database.EnsureCreatedAsync();

        var planet = new Exoplanet
        {
            Name = "Example b",
            Hostname = "Old host",
            DensityGPerCm3 = 7,
            DiscoveryYear = 2000,
            StarLuminosityLog10RelativeToSun = 2,
            StarSpectralType = "Old type"
        };
        db.Exoplanets.Add(planet);
        await db.SaveChangesAsync();
        var originalId = planet.Id;
        db.ChangeTracker.Clear();

        const string json = """
            [{
                "pl_name": "Example b",
                "hostname": "New host",
                "pl_dens": 3.5,
                "disc_year": 2020,
                "discoverymethod": "Transit",
                "st_lum": -0.5,
                "st_spectype": null
            }]
            """;
        using var http = new HttpClient(new JsonResponseHandler(json));
        var service = new ExoplanetSyncService(new NasaExoplanetArchiveClient(http), db);

        await service.SyncTransitPlanetsAsync(CancellationToken.None);
        db.ChangeTracker.Clear();

        var saved = await db.Exoplanets.SingleAsync();
        Assert.Equal(originalId, saved.Id);
        Assert.Equal("New host", saved.Hostname);
        Assert.Equal(3.5, saved.DensityGPerCm3);
        Assert.Equal((short)2020, saved.DiscoveryYear);
        Assert.Equal("Transit", saved.DiscoveryMethod);
        Assert.Equal(-0.5, saved.StarLuminosityLog10RelativeToSun);
        Assert.Null(saved.StarSpectralType);
    }

    private sealed class JsonResponseHandler(string json) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });
        }
    }
}
