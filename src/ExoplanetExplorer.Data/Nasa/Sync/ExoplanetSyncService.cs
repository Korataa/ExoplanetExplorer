using Microsoft.EntityFrameworkCore;

public sealed class ExoplanetSyncService
{
    private readonly NasaExoplanetArchiveClient nasaClient;
    private readonly ExoplanetDbContext dbContext;

    public ExoplanetSyncService(
        NasaExoplanetArchiveClient nasaClient,
        ExoplanetDbContext dbContext)
    {
        this.nasaClient = nasaClient;
        this.dbContext = dbContext;
    }

    public async Task SyncTransitPlanetsAsync(CancellationToken cancellationToken)
    {
        var dtos = await nasaClient.GetPlanetsAsync(cancellationToken);
        var existing = await dbContext.Exoplanets.ToDictionaryAsync(
            planet => planet.Name, cancellationToken);

        var seenNames = new HashSet<string>();

        foreach (var dto in dtos)
        {
            var incoming = NasaExoplanetMapper.ToModel(dto);

            if (!seenNames.Add(incoming.Name))
                throw new InvalidOperationException(
                $"Duplicate NASA planet name: {incoming.Name}");

            if(existing.TryGetValue(incoming.Name, out var saved))
            {
                saved.Name = incoming.Name;
                saved.Hostname = incoming.Hostname;
                saved.MassEarth = incoming.MassEarth;
                saved.RadiusEarth = incoming.RadiusEarth;
                saved.DensityGPerCm3 = incoming.DensityGPerCm3;
                saved.OrbitalPeriod = incoming.OrbitalPeriod;
                saved.OrbitalSemiMajorAxisAu = incoming.OrbitalSemiMajorAxisAu;
                saved.OrbitalEccentricity = incoming.OrbitalEccentricity;
                saved.EquilibriumTemperatureK = incoming.EquilibriumTemperatureK;
                saved.IncidentStellarFluxEarth = incoming.IncidentStellarFluxEarth;
                saved.DiscoveryYear = incoming.DiscoveryYear;
                saved.DiscoveryPubDate = incoming.DiscoveryPubDate;
                saved.DiscoveryMethod = incoming.DiscoveryMethod;
                saved.DiscoveryFacility = incoming.DiscoveryFacility;
                saved.DiscoveryTelescope = incoming.DiscoveryTelescope;
                saved.DiscoveryInstrument = incoming.DiscoveryInstrument;
                saved.DiscoveryLocale = incoming.DiscoveryLocale;
                saved.DiscoveryReferenceName = incoming.DiscoveryReferenceName;
                saved.SystemDistanceParsecs = incoming.SystemDistanceParsecs;
                saved.SystemStarCount = incoming.SystemStarCount;
                saved.SystemPlanetCount = incoming.SystemPlanetCount;
                saved.StarMassSolar = incoming.StarMassSolar;
                saved.StarRadiusSolar = incoming.StarRadiusSolar;
                saved.StarEffectiveTemperatureK = incoming.StarEffectiveTemperatureK;
                saved.StarLuminosityLog10RelativeToSun = incoming.StarLuminosityLog10RelativeToSun;
                saved.StarSpectralType = incoming.StarSpectralType;
            }
            else
            {
                dbContext.Exoplanets.Add(incoming);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}