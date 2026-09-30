using System.Collections.Generic;

// Добыча за раунд: что погружено в кузов
public class ArmorLoot
{
    private readonly Dictionary<ScrapType, int> counts = new();

    public int armor { get; private set; }
    public int weight { get; private set; }

    public int extraFuel {
        get { return weight * ShipParametersConsts.baseArmorCofficient; }
    }

    // Чугунный хлам в кузов не грузится, тогда вернёт false
    public bool add(ScrapType type) {
        ScrapParameters parameters = ArmorCollectionConsts.parameters(type);
        if (!parameters.loadable) {
            return false;
        }

        armor += parameters.armor;
        weight += parameters.weight;
        counts[type] = count(type) + 1;
        return true;
    }

    public int count(ScrapType type) {
        return counts.TryGetValue(type, out int value) ? value : 0;
    }

    // Итог раунда заменяет прошлый результат, а не прибавляется к нему
    public SpaceShipState applyTo(SpaceShipState state) {
        return state.copy(
            armorCollected: armor,
            armorWeight: weight
        );
    }
}
