interface ShipBaseParametersCalculator
{
    int calculateBaseArmorNeededForDistance(int distance, int obstacles);
    int calculateBaseFoodNeededForDistance(int distance);

    int calculateFuelNeededForDistance(int distance, int obstacles, int foodWeight, int armorWeight);
}
