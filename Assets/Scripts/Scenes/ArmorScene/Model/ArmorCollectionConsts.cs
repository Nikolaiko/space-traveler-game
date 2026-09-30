using System.Collections.Generic;

public static class ArmorCollectionConsts {
    // Все пластины дают одинаковую броню и различаются весом,
    // а каждая единица веса брони стоит baseArmorCofficient топлива
    public static readonly Dictionary<ScrapType, ScrapParameters> scrapParameters = new() {
        { ScrapType.titanium, new ScrapParameters(armor: 2, weight: 1, magnetic: false) },
        { ScrapType.steel, new ScrapParameters(armor: 2, weight: 2, magnetic: true) },
        { ScrapType.rustyIron, new ScrapParameters(armor: 2, weight: 4, magnetic: true) },
        { ScrapType.junk, new ScrapParameters(armor: 0, weight: 3, magnetic: false) },
        { ScrapType.castIron, new ScrapParameters(armor: 0, weight: 0, magnetic: true, loadable: false) }
    };

    public static ScrapParameters parameters(ScrapType type) {
        return scrapParameters[type];
    }
}
