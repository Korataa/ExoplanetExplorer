using System.Net.Http;

using var http = new HttpClient();

var adql = """
    SELECT pl_name, discoverymethod, pl_bmasse, pl_rade, pl_orbper
    FROM pscomppars
    WHERE discoverymethod = 'Transit'
    """;

var url = "https://exoplanetarchive.ipac.caltech.edu/TAP/sync"
    + $"?query={Uri.EscapeDataString(adql)}"
    + "&format=json";

var response = await http.GetAsync(url);
response.EnsureSuccessStatusCode();

var json = await response.Content.ReadAsStringAsync();

List<Exoplanet> exoplanets = 
    System.Text.Json.JsonSerializer.Deserialize<List<Exoplanet>>(json)
    ?? throw new InvalidOperationException("Failed to deserialize exoplanets from JSON");

foreach (Exoplanet planet in exoplanets)
{
    Console.WriteLine($"Name: {planet.Name}");
    Console.WriteLine($"Discovery Method: {planet.DiscoveryMethod}");
    Console.WriteLine($"Mass: {planet.Mass}");
    Console.WriteLine($"Radius: {planet.Radius}");
    Console.WriteLine($"Orbital Period: {planet.OrbitalPeriod}");
    Console.WriteLine();
}

Console.WriteLine($"Total Exoplanets found with transit method: {exoplanets.Count}");