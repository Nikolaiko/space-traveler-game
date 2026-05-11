namespace PlanetTypeExtensions {
    public static class PlanetTypeExtensions {
        public static string getPlanetImageResourcePath(this DestinationPlanetType planetType) {
            switch (planetType) {
                case DestinationPlanetType.Blue: {
                    return "Images/Planets/blue";
                }
                case DestinationPlanetType.Purple: {
                    return "Images/Planets/brown";
                }
                case DestinationPlanetType.Green: {
                    return "Images/Planets/green";                    
                }
                case DestinationPlanetType.Red: {
                    return "Images/Planets/red";                    
                }
                default: {
                    return "Images/Planets/blue";
                }
            }
        }
    }
}