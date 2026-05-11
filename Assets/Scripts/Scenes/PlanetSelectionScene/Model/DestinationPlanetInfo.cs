using System.ComponentModel;

[ImmutableObject(true)]
public struct DestinationPlanetInfo
{
    public DestinationPlanetInfo(int distance, int obstacles, DestinationPlanetType planetType) {
        this.distance = distance;
        this.obstacles = obstacles;
        this.planetType = planetType;
    }

    public readonly DestinationPlanetType planetType;
    public readonly int distance;
    public readonly int obstacles;
}
