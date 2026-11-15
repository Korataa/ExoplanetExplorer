using Xunit;
using System.Collections.Generic;

public class ExoplanetDeserializationTests
{
    [Fact]
    public void DeserializesArchiveJsonIntoExoplanet()
    {
        const string json = """
            [{
            "pl_name": "TOI-7176 b",
            "discoverymethod": "Transit",
            "pl_bmasse": 200.86755322,
            "pl_rade": 10.5,
            "pl_orbper": 5.2
            }]
            """;

        var planets = System.Text.Json.JsonSerializer.Deserialize<List<Exoplanet>>(json);

        Assert.NotNull(planets);
        var planet = Assert.Single(planets);
        Assert.Equal("TOI-7176 b", planet.Name);
        Assert.Equal("Transit", planet.DiscoveryMethod);
        Assert.Equal(200.86755322, planet.Mass);
        Assert.Equal(10.5, planet.Radius);
        Assert.Equal(5.2, planet.OrbitalPeriod);
    }
}