using System.Text.Json.Serialization;

public class Exoplanet
{
    [JsonPropertyName("pl_name")]
    public required string Name { get; set; }
    [JsonPropertyName("discoverymethod")]
    public required string DiscoveryMethod { get; set; }
    [JsonPropertyName("pl_bmasse")]
    public double? Mass { get; set; }
    [JsonPropertyName("pl_rade")]
    public double? Radius { get; set; }
    [JsonPropertyName("pl_orbper")]
    public double? OrbitalPeriod { get; set; }
    
}