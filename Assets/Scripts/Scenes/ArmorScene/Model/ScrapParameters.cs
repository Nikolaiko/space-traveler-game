using System.ComponentModel;

[ImmutableObject(true)]
public struct ScrapParameters
{
    public ScrapParameters(int armor, int weight, bool magnetic, bool loadable = true) {
        this.armor = armor;
        this.weight = weight;
        this.magnetic = magnetic;
        this.loadable = loadable;
    }

    public readonly int armor;
    public readonly int weight;
    public readonly bool magnetic;
    public readonly bool loadable;
}
