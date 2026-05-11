using UnityEngine;

public class GameStateManager
{
    private static string armorNeededKeyName = "armor_needed";
    private static string armorCollectedKeyName = "armor_collected";
    private static string armorWeightKeyName = "armor_weight";

    private static string foodNeededKeyName = "food_needed";
    private static string foodCollectedKeyName = "food_collected";
    private static string foodWeightKeyName = "food_weight";

    private static string fuelKeyName = "fuel_collected";

    public void initGameValues(int neededArmor, int neededFood) {
        setArmourNeeded(neededArmor);
        setFoodNeeded(neededFood);
        
        setFoodWeight(0);
        setArmourCollected(0);
        setArmourWeight(0);
        setFuelCollected(0);
        setFoodCollected(0);

        PlayerPrefs.Save();
    }
    
    public void setArmourNeeded(int armorValue) {
        PlayerPrefs.SetInt(armorNeededKeyName, armorValue);
        PlayerPrefs.Save();
    }

    public void setFoodNeeded(int foodValue) {
        PlayerPrefs.SetInt(foodNeededKeyName, foodValue);
        PlayerPrefs.Save();
    }

    public void setFoodCollected(int foodValue) {
        PlayerPrefs.SetInt(foodCollectedKeyName, foodValue);
        PlayerPrefs.Save();
    }

    public void setFuelCollected(int fuelValue) {
        PlayerPrefs.SetInt(fuelKeyName, fuelValue);
        PlayerPrefs.Save();
    }

    public void setArmourCollected(int armorValue) {
        PlayerPrefs.SetInt(armorCollectedKeyName, armorValue);
        PlayerPrefs.Save();
    }

    public void setArmourWeight(int armorWeight) {
        PlayerPrefs.SetInt(armorWeightKeyName, armorWeight);
        PlayerPrefs.Save();
    }

    public void setFoodWeight(int foodWeight) {
        PlayerPrefs.SetInt(foodWeightKeyName, foodWeight);
        PlayerPrefs.Save();
    }

    public int getFoodWeight() {
        return PlayerPrefs.GetInt(foodWeightKeyName);
    }

    public int getFoodNeeded() {
        return PlayerPrefs.GetInt(foodNeededKeyName);
    }

    public int getFoodCollected() {
        return PlayerPrefs.GetInt(foodCollectedKeyName);
    }

    public int getFuelCollected() {
        return PlayerPrefs.GetInt(fuelKeyName);
    }

    public int getArmorNeeded() {
        return PlayerPrefs.GetInt(armorNeededKeyName);
    }

    public int getArmorCollected() {
        return PlayerPrefs.GetInt(armorCollectedKeyName);
    }

    public int getArmorWeight() {
        return PlayerPrefs.GetInt(armorWeightKeyName);
    }

    public void resetProgress() {
        PlayerPrefs.DeleteKey(armorNeededKeyName);
        PlayerPrefs.DeleteKey(armorCollectedKeyName);
        PlayerPrefs.DeleteKey(armorWeightKeyName);

        PlayerPrefs.DeleteKey(foodNeededKeyName);
        PlayerPrefs.DeleteKey(foodWeightKeyName);
    }
}
