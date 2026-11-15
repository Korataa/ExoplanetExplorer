public class Exoplanet
{
    // Identification
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Hostname { get; set; }

    // Planet Properties
    public double? MassEarth { get; set; }
    public double? RadiusEarth { get; set; }
    public double? DensityGPerCm3 { get; set; }
    public double? OrbitalPeriod { get; set; }
    public double? OrbitalSemiMajorAxisAu { get; set; }
    public double? OrbitalEccentricity { get; set; }
    public double? EquilibriumTemperatureK { get; set; }
    public double? IncidentStellarFluxEarth { get; set; }

    // Discovery Information
    public short? DiscoveryYear { get; set; }
    public string? DiscoveryPubDate { get; set; }
    public string? DiscoveryMethod { get; set; }
    public string? DiscoveryFacility { get; set; }
    public string? DiscoveryTelescope { get; set; }
    public string? DiscoveryInstrument { get; set; }
    public string? DiscoveryLocale { get; set; }
    public string? DiscoveryReferenceName { get; set; }

    // Host Star Properties
    public double? SystemDistanceParsecs { get; set; }
    public int? SystemStarCount { get; set; }
    public int? SystemPlanetCount { get; set; }
    public double? StarMassSolar { get; set; }
    public double? StarRadiusSolar { get; set; }
    public double? StarEffectiveTemperatureK { get; set; }
    public double? StarLuminosityLog10RelativeToSun { get; set; }
    public string? StarSpectralType { get; set; }
}