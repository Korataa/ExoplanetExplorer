public static class NasaExoplanetMapper
{
    public static Exoplanet ToModel(NasaExoplanetDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Exoplanet name is required.", nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Hostname))
            throw new ArgumentException("Exoplanet hostname is required.", nameof(dto));

        return new Exoplanet
        {
            Name = dto.Name,
            Hostname = dto.Hostname,
            MassEarth = dto.MassEarth,
            RadiusEarth = dto.RadiusEarth,
            DensityGPerCm3 = dto.DensityGPerCm3,
            OrbitalPeriod = dto.OrbitalPeriod,
            OrbitalSemiMajorAxisAu = dto.OrbitalSemiMajorAxisAu,
            OrbitalEccentricity = dto.OrbitalEccentricity,
            EquilibriumTemperatureK = dto.EquilibriumTemperatureK,
            IncidentStellarFluxEarth = dto.IncidentStellarFluxEarth,
            DiscoveryYear = dto.DiscoveryYear,
            DiscoveryPubDate = dto.DiscoveryPubDate,
            DiscoveryMethod = dto.DiscoveryMethod,
            DiscoveryFacility = dto.DiscoveryFacility,
            DiscoveryTelescope = dto.DiscoveryTelescope,
            DiscoveryInstrument = dto.DiscoveryInstrument,
            DiscoveryLocale = dto.DiscoveryLocale,
            DiscoveryReferenceName = dto.DiscoveryReferenceName,
            SystemDistanceParsecs = dto.SystemDistance,
            SystemStarCount = dto.SystemStarCount,
            SystemPlanetCount = dto.SystemPlanetCount,
            StarMassSolar = dto.StarMass,
            StarRadiusSolar = dto.StarRadius,
            StarEffectiveTemperatureK = dto.StarEffectiveTemperature,
            StarLuminosityLog10RelativeToSun = dto.StarLuminosity,
            StarSpectralType = dto.StarSpectralType
        };
    }
}