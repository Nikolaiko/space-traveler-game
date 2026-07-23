using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CalculateScreenUI : MonoBehaviour
{
    [Inject]
    private PlanetsInfoLoader planetsInfoLoader;

    [Inject]
    private LocalDataManager localDataManager;

    public TMP_Text distanceText;
    public TMP_Text obstaclesText;
    public TMP_Text foodStatusText;
    public TMP_Text fuelStatusText;
    public TMP_InputField fuelValue;
    public Button saveEnteredValueButton;
    public Action onParametersScreenClosed;
    public Action onParametersScreenUpdateValue;

    private SpaceShipState screenState;

    public void Awake() {        
        fuelValue.onValidateInput += onChangeFuelValue;
    }

    public void onCloseScreen() {
        if (onParametersScreenClosed != null) {
            onParametersScreenClosed();
        }
    }

    public void showScreen(SpaceShipState shipState) {
        screenState = shipState;
        List<DestinationPlanetInfo> planetInfos = planetsInfoLoader.loadPlanetsInfo();

        try {
            DestinationPlanetInfo info = planetInfos.Where(
                value => value.planetType == shipState.planetType).First();
            distanceText.text = "Расстояние: " + info.distance;
            obstaclesText.text = "Препятствия: " + info.obstacles;           

            if (screenState.foodCollected >= screenState.foodNeeded)
            {
                foodStatusText.color = Color.green;
            } else
            {
                foodStatusText.color = Color.red;
            }

            if (screenState.fuelCollected >= screenState.fuelNeeded)
            {
                fuelStatusText.color = Color.green;
            } else
            {
                fuelStatusText.color = Color.red;
            }   
            
            fuelStatusText.text = $"Топлива собрано: {screenState.fuelCollected}";
            foodStatusText.text = $"Еды собрано: {screenState.foodCollected}, вес: {screenState.foodWeight}";
            if (screenState.fuelNeeded.HasValue) {
                fuelValue.text = screenState.fuelNeeded.Value.ToString();
                saveEnteredValueButton.enabled = true;
            } else
            {
                saveEnteredValueButton.enabled = false;
            }

        } catch (ArgumentNullException argumentException) {
            Debug.LogException(argumentException);
        } catch (InvalidOperationException operationException) {
            Debug.LogException(operationException);
        }

        gameObject.SetActive(true);
    }

    public void showFuelValueScreen() {
    }

    public void hideScreen() {        
        gameObject.SetActive(false);
    }

    public char onChangeFuelValue(string value, int charIndex, char addedChar) {
        print(addedChar);
        if (char.IsDigit(addedChar)) {
            saveEnteredValueButton.enabled = true;
            return addedChar;
        } else {
            if (value.Length <= 0) { saveEnteredValueButton.enabled = false; }
            return '\0';
        }
    }

    public void onApply() {
        int prasedValue;
        int? finalValue = null;
        if (int.TryParse(fuelValue.text, out prasedValue)) {
            finalValue = prasedValue;
        }

        localDataManager.saveGameState(screenState.copy(fuelNeeded: finalValue));
        onParametersScreenUpdateValue?.Invoke();

        screenState = screenState.copy(fuelNeeded: finalValue);

        if (screenState.fuelCollected >= screenState.fuelNeeded) {
            fuelStatusText.color = Color.green;
        } else {
            fuelStatusText.color = Color.red;
        }
    }

    public void fuelValueChanged(String newValue) {
        saveEnteredValueButton.interactable = newValue.Length > 0;
    }
}
