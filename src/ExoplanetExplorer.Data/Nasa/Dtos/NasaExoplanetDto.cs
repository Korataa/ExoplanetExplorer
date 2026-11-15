using System.Text.Json.Serialization;

public class NasaExoplanetDto
{
    //Planet Identification
    [JsonPropertyName("pl_name")]
    public required string Name { get; set; }
    [JsonPropertyName("hostname")]
    public required string Hostname { get; set; }

    //Planet Properties
    [JsonPropertyName("pl_bmasse")]
    public double? MassEarth { get; set; }
    [JsonPropertyName("pl_rade")]
    public double? RadiusEarth { get; set; }
    [JsonPropertyName("pl_dens")]
    public double? DensityGPerCm3 { get; set; }
    [JsonPropertyName("pl_orbper")]
    public double? OrbitalPeriod { get; set; }
    [JsonPropertyName("pl_orbsmax")]
    public double? OrbitalSemiMajorAxisAu { get; set; }
    [JsonPropertyName("pl_orbeccen")]
    public double? OrbitalEccentricity { get; set; }
    [JsonPropertyName("pl_eqt")]
    public double? EquilibriumTemperatureK { get; set; }
    [JsonPropertyName("pl_insol")]
    public double? IncidentStellarFluxEarth { get; set; }



    //Discovery Information
    [JsonPropertyName("disc_year")]
    public short? DiscoveryYear { get; set; }
    [JsonPropertyName("disc_pubdate")]
    public string? DiscoveryPubDate { get; set; }
    [JsonPropertyName("discoverymethod")]
    public required string DiscoveryMethod { get; set; }
    [JsonPropertyName("disc_facility")]
    public string? DiscoveryFacility { get; set; }
    [JsonPropertyName("disc_telescope")]
    public string? DiscoveryTelescope { get; set; }
    [JsonPropertyName("disc_instrument")]
    public string? DiscoveryInstrument { get; set; }
    [JsonPropertyName("disc_locale")]
    public string? DiscoveryLocale { get; set; }
    [JsonPropertyName("disc_refname")]
    public string? DiscoveryReferenceName { get; set; }

    //Host Star Properties
    [JsonPropertyName("sy_dist")]
    public double? SystemDistance { get; set; }
    [JsonPropertyName("sy_snum")]
    public int? SystemStarCount { get; set; }
    [JsonPropertyName("sy_pnum")]
    public int? SystemPlanetCount { get; set; }
    [JsonPropertyName("st_mass")]
    public double? StarMass { get; set; }
    [JsonPropertyName("st_rad")]
    public double? StarRadius { get; set; }
    [JsonPropertyName("st_teff")]
    public double? StarEffectiveTemperature { get; set; }
    [JsonPropertyName("st_lum")]
    public double? StarLuminosity { get; set; }
    [JsonPropertyName("st_spectype")]
    public string? StarSpectralType { get; set; }
    
}