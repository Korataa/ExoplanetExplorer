using System.Net.Http.Json;

public sealed class NasaExoplanetArchiveClient
{
    private readonly HttpClient httpClient = new HttpClient();

    public NasaExoplanetArchiveClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<List<NasaExoplanetDto>> GetPlanetsAsync(CancellationToken cancellationToken = default)
    {
        var query = """
            SELECT pl_name, hostname, pl_bmasse, pl_rade, pl_dens, pl_orbper,
            pl_orbsmax, pl_orbeccen, pl_eqt, pl_insol, disc_year, disc_pubdate,
            discoverymethod, disc_facility, disc_telescope, disc_instrument,
            disc_locale, disc_refname, sy_dist, sy_snum, sy_pnum, st_mass, st_rad,
            st_teff, st_lum, st_spectype
            FROM pscomppars
            """;

        var url = "https://exoplanetarchive.ipac.caltech.edu/TAP/sync"
            + $"?query={Uri.EscapeDataString(query)}"
            + "&format=json";

        using var response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<NasaExoplanetDto>>(
            cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("NASA returned no planet data!");
    }
}