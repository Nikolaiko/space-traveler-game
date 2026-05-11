public class SimpleShipParametersCalculator : ShipBaseParametersCalculator
{
    public int calculateBaseArmorNeededForDistance(int distance, int obstacles) {
        return distance * ShipParametersConsts.baseArmorCofficient + obstacles * ShipParametersConsts.baseObstaclesCofficient;
    }

    public int calculateBaseFoodNeededForDistance(int distance) {
        return distance * ShipParametersConsts.baseFoodCofficient;
    }

    public int calculateFuelNeededForDistance(int distance, int obstacles, int foodWeight, int armorWeight) {
        return distance * ShipParametersConsts.baseFueldCofficient +
               foodWeight * ShipParametersConsts.baseFoodCofficient +
               armorWeight * ShipParametersConsts.baseArmorCofficient +
               obstacles * ShipParametersConsts.baseObstaclesCofficient;
    }
}
