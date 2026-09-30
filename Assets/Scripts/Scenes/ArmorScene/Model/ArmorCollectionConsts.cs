using System;
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

    #region Раунд и конвейер

    public static readonly float roundDuration = 50f;

    // Скорость ленты в единицах мира в секунду, растёт от начала к концу раунда
    public static readonly float beltStartSpeed = 1.2f;
    public static readonly float beltEndSpeed = 2.8f;

    // Просвет между соседними предметами на ленте, выбирается случайно в этих пределах
    public static readonly float minItemGap = 0.6f;
    public static readonly float maxItemGap = 1.6f;

    #endregion

    #region Мешок металлолома

    // Сколько предметов каждого типа в одном мешке. Предметы тянутся из перемешанного
    // мешка, а не случайными бросками, поэтому доли типов держатся на коротком отрезке
    public static readonly Dictionary<ScrapType, int> scrapBagBase = new() {
        { ScrapType.titanium, 2 },
        { ScrapType.steel, 5 },
        { ScrapType.rustyIron, 5 },
        { ScrapType.junk, 3 }
    };

    // Чугунного хлама в мешке тем больше, чем больше препятствий у планеты
    public static readonly float castIronPerObstacle = 0.5f;

    public static readonly int maxSameTypeInRow = 2;

    #endregion

    public static ScrapParameters parameters(ScrapType type) {
        return scrapParameters[type];
    }

    public static Dictionary<ScrapType, int> scrapBagContents(int obstacles) {
        Dictionary<ScrapType, int> contents = new(scrapBagBase);
        contents[ScrapType.castIron] = (int)Math.Ceiling(obstacles * castIronPerObstacle);
        return contents;
    }
}
