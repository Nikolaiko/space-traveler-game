using System;
using System.Collections.Generic;
using System.Linq;
using ListTypeExtensions;

// Перемешанный мешок металлолома: в каждом мешке заданное число предметов
// каждого типа, опустевший мешок наполняется заново
public class ScrapBag
{
    private readonly Dictionary<ScrapType, int> contents;
    private readonly int maxSameTypeInRow;
    private readonly Random random;
    private readonly bool hasSeveralTypes;
    // Предметы берутся с конца списка, следующий мешок кладётся в начало
    private readonly List<ScrapType> items = new();

    private ScrapType? lastType;
    private int sameTypeInRow;

    public ScrapBag(Dictionary<ScrapType, int> contents, int maxSameTypeInRow, Random random) {
        this.contents = contents;
        this.maxSameTypeInRow = maxSameTypeInRow;
        this.random = random;
        hasSeveralTypes = contents.Count(entry => entry.Value > 0) > 1;
    }

    public ScrapType next() {
        if (items.Count == 0) {
            refill();
        }

        int index = items.Count - 1;
        if (items[index] == lastType && sameTypeInRow >= maxSameTypeInRow && hasSeveralTypes) {
            // Серия слишком длинная: берём предмет другого типа. Если в мешке
            // остался только этот тип, берём его из следующего мешка
            int otherIndex = items.FindLastIndex(type => type != lastType);
            if (otherIndex < 0) {
                refill();
                index = items.Count - 1;
                otherIndex = items.FindLastIndex(type => type != lastType);
            }
            (items[index], items[otherIndex]) = (items[otherIndex], items[index]);
        }

        ScrapType next = items[index];
        items.RemoveAt(index);

        sameTypeInRow = next == lastType ? sameTypeInRow + 1 : 1;
        lastType = next;
        return next;
    }

    private void refill() {
        List<ScrapType> bag = new();
        foreach (KeyValuePair<ScrapType, int> entry in contents) {
            for (int i = 0; i < entry.Value; i++) {
                bag.Add(entry.Key);
            }
        }

        if (bag.Count == 0) {
            throw new InvalidOperationException("ScrapBag: bag contents are empty");
        }

        bag.Shuffle(random);
        items.InsertRange(0, bag);
    }
}
