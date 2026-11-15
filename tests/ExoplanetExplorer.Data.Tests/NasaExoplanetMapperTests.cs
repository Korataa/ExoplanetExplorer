using System.Text.Json;
namespace ExoplanetExplorer.Data.Tests.NASA;

public class NasaExoplanetMapperTests
{
    [Fact]
    public async Task DeserializeAndMapNasaResponseReturnsCorrectExoplanets()
    {
        // Arrange
        string filePath = Path.Combine(
            AppContext.BaseDirectory,
            "Fixtures",
            "NasaExoplanets.json"
        );

        string json = await File.ReadAllTextAsync(filePath);

        // Act - Deserialize NASA response
        List<NasaExoplanetDto>? dtos =
            JsonSerializer.Deserialize<List<NasaExoplanetDto>>(json);

        // Assert - Deserialization
        Assert.NotNull(dtos);
        Assert.NotEmpty(dtos);

        // Act - Map DTOs to application models
        List<Exoplanet> planets = dtos
            .Select(NasaExoplanetMapper.ToModel)
            .ToList();

        // Assert - Mapping
        Assert.Equal(dtos.Count, planets.Count);

        for (int i = 0; i < dtos.Count; i++)
        {
            var dto = dtos[i];
            var planet = planets[i];

            Assert.Equal(dto.Name, planet.Name);
            Assert.Equal(dto.Hostname, planet.Hostname);

            Assert.Equal(dto.MassEarth, planet.MassEarth);
            Assert.Equal(dto.RadiusEarth, planet.RadiusEarth);
            Assert.Equal(dto.DensityGPerCm3, planet.DensityGPerCm3);

            Assert.Equal(dto.OrbitalPeriod, planet.OrbitalPeriod);
            Assert.Equal(dto.OrbitalSemiMajorAxisAu, planet.OrbitalSemiMajorAxisAu);
            Assert.Equal(dto.OrbitalEccentricity, planet.OrbitalEccentricity);

            Assert.Equal(dto.EquilibriumTemperatureK, planet.EquilibriumTemperatureK);
            Assert.Equal(dto.IncidentStellarFluxEarth, planet.IncidentStellarFluxEarth);

            Assert.Equal(dto.DiscoveryYear, planet.DiscoveryYear);
            Assert.Equal(dto.DiscoveryPubDate, planet.DiscoveryPubDate);
            Assert.Equal(dto.DiscoveryMethod, planet.DiscoveryMethod);

            Assert.Equal(dto.DiscoveryFacility, planet.DiscoveryFacility);
            Assert.Equal(dto.DiscoveryTelescope, planet.DiscoveryTelescope);
            Assert.Equal(dto.DiscoveryInstrument, planet.DiscoveryInstrument);
            Assert.Equal(dto.DiscoveryLocale, planet.DiscoveryLocale);
            Assert.Equal(dto.DiscoveryReferenceName, planet.DiscoveryReferenceName);

            Assert.Equal(dto.SystemDistance, planet.SystemDistanceParsecs);
            Assert.Equal(dto.SystemStarCount, planet.SystemStarCount);
            Assert.Equal(dto.SystemPlanetCount, planet.SystemPlanetCount);

            Assert.Equal(dto.StarMass, planet.StarMassSolar);
            Assert.Equal(dto.StarRadius, planet.StarRadiusSolar);
            Assert.Equal(dto.StarEffectiveTemperature, planet.StarEffectiveTemperatureK);
            Assert.Equal(dto.StarLuminosity, planet.StarLuminositySolar);
            Assert.Equal(dto.StarSpectralType, planet.StarSpectralType);
        }
    }
}