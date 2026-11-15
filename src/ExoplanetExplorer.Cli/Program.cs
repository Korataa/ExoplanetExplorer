using System.Net.Http;

using var http = new HttpClient();

var adql = """
    SELECT pl_name, hostname, discoverymethod, pl_bmasse, pl_rade, pl_orbper
    FROM pscomppars
    WHERE discoverymethod = 'Transit'
    """;

var url = "https://exoplanetarchive.ipac.caltech.edu/TAP/sync"
    + $"?query={Uri.EscapeDataString(adql)}"
    + "&format=json";

var response = await http.GetAsync(url);
response.EnsureSuccessStatusCode();

var json = await response.Content.ReadAsStringAsync();

List<NasaExoplanetDto> dtos =
    System.Text.Json.JsonSerializer.Deserialize<List<NasaExoplanetDto>>(json)
    ?? throw new InvalidOperationException("Failed to deserialize exoplanets from JSON");

List<Exoplanet> exoplanets = dtos
    .Select(NasaExoplanetMapper.ToModel)
    .ToList();

foreach (Exoplanet planet in exoplanets)
{
    Console.WriteLine($"Name: {planet.Name}");
    Console.WriteLine($"Discovery Method: {planet.DiscoveryMethod}");
    Console.WriteLine($"Mass: {planet.MassEarth}");
    Console.WriteLine($"Radius: {planet.RadiusEarth}");
    Console.WriteLine($"Orbital Period: {planet.OrbitalPeriod}");
    Console.WriteLine();
}

Console.WriteLine($"Total Exoplanets found with transit method: {exoplanets.Count}");