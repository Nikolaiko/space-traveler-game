using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class EnterFuelValueScreenUI : MonoBehaviour
{
    [Inject]
    private LocalDataManager localDataManager;
    [Inject]
    private PlanetsInfoLoader planetsInfoLoader;

    private SpaceShipState state;

    public Action<int?> onApplyChange;
    public TMP_InputField fuelValue;
    public TMP_Text formulaText;
    public TMP_Text distanceText;
    public TMP_Text obstaclesText;
    public TMP_Text foodWeightText;
    public TMP_Text armorWeightText;

    public void Awake() {
        fuelValue.onValidateInput += onChangeFuelValue;

        formulaText.text = String.Format(
            ShipParametersConsts.fuelFormulaString,
            ShipParametersConsts.baseFueldCofficient,
            ShipParametersConsts.baseObstaclesCofficient,
            ShipParametersConsts.baseFoodCofficient,
            ShipParametersConsts.baseArmorCofficient
        );
    }

    public void showScreen() {
        state = localDataManager.getSavedState().GetValueOrDefault();
        List<DestinationPlanetInfo> planetInfos = planetsInfoLoader.loadPlanetsInfo();

        try {
            DestinationPlanetInfo info = planetInfos.Where(
                value => value.planetType == state.planetType
            ).First();

            distanceText.text = "Расстояние: " + info.distance;
            obstaclesText.text = "Препятствия: " + info.obstacles;

            foodWeightText.text = $"Вес еды: {state.foodWeight}";
            armorWeightText.text = $"Вес брони: {state.armorWeight}";

            if (state.fuelNeeded.HasValue) {
                fuelValue.text = state.fuelNeeded.Value.ToString();
            } else {
                fuelValue.text = "????";
            }

        } catch (ArgumentNullException argumentException) {
            Debug.LogException(argumentException);
        } catch (InvalidOperationException operationException) {
            Debug.LogException(operationException);
        }

        gameObject.SetActive(true);   
    }

    public char onChangeFuelValue(string value, int charIndex, char addedChar) {
        if (char.IsDigit(addedChar)) {
            return addedChar;
        } else {
            return '\0';
        }
    }

    public void onCancel() {
        gameObject.SetActive(false);
    }

    public void onApply() {
        int prasedValue;
        int? finalValue = null;
        if (int.TryParse(fuelValue.text, out prasedValue)) {
            finalValue = prasedValue;
        }

        localDataManager.saveGameState(state.copy(fuelNeeded: finalValue));
        onApplyChange(finalValue);
        gameObject.SetActive(false);
    }
}
