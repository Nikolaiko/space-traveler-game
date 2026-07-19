using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameProgressUI : MonoBehaviour
{
    [Inject]
    private TipsScreenUIManager tipsScreenUIManager;

    [Inject]
    private LocalDataManager dataManager;
    
    #region UI Actions

    public Action<GameTipType> onTipScreenClosed;
    public Action onFuelButtonTap;
    public Action onLaunchButtonTap;
    public Action onFoodButtonTap;
    public Action onShipParameterClose;
    public Action onBannerCloseButtonTap;

    #endregion
    public CalculateScreenUI calculateScreen;
    public BasicUIScreen armorDevelopmentBanner;

    #region UI Elements

    public Button fuelButton;
    public Button foodButton;
    public Button launchButton;
    public Button shipParametersButton;
    public GameObject mainUI;

    public CheckBox foodCheckBox;
    public CheckBox fuelCheckBox;
    public SelectedPlanedObject selectedPlanedObject;

    #endregion

    public void Awake() {
        tipsScreenUIManager.onTipScreenClosed = tipScreenClosed;
        calculateScreen.onParametersScreenClosed = hideShipParametersScreen;
        armorDevelopmentBanner.onCloseScreen = onArmorDevelopmentBannerClose;

        SpaceShipState? state = dataManager.getSavedState();
        if (state.HasValue)
        {
            selectedPlanedObject.setSelectedPlanet(state.Value.planetType);
        }
    }

    public void showTip(GameTipType tipType) {
        tipsScreenUIManager.showTip(tipType);  
    }

    public void showArmorDevelopmentBanner() {
        armorDevelopmentBanner.show();
    }

    public void showShipParametersScreen(SpaceShipState shipState) {
        mainUI.SetActive(false);
        calculateScreen.showScreen(shipState);
    }

    public void fuelButtonClick() {
        onFuelButtonTap?.Invoke();
    }

    public void launchButtonClick() {
        onLaunchButtonTap?.Invoke();
    }

    public void shipButtonClick() {
        onShipParameterClose?.Invoke();
    }

    public void foodButtonClick() {
        onFoodButtonTap?.Invoke();
    }

    public void setFuelQuestButtonEnabled(bool isEnabled) {
        fuelButton.interactable = isEnabled;
    }

    public void setFoodQuestButtonEnabled(bool isEnabled) {
        foodButton.interactable = isEnabled;
    }

    public void disbaleLaunchButton() {
        launchButton.interactable = false;
    }

    public void enableLaunchButton() {
        launchButton.interactable = true;
    }

    public void disbaleShipParameterButton() {
        shipParametersButton.interactable = false;
    }

    public void enableShipParameterButton() {
        shipParametersButton.interactable = true;
    }

    public void setFoodState(CollectionState state) {
        foodCheckBox.setSelected(state == CollectionState.collected);
    }

    public void setFoodWeight(int weight) {
        //foodWeightText.text += weight.ToString();
    }

    public void setFuelState(CollectionState state) {
        fuelCheckBox.setSelected(state == CollectionState.collected);
    }

    private void hideShipParametersScreen() {
        calculateScreen.hideScreen();
        mainUI.SetActive(true);
        onShipParameterClose?.Invoke();
    }

    private void tipScreenClosed(GameTipType tipType) {
        onTipScreenClosed(tipType);
    }

    private void onArmorDevelopmentBannerClose() {        
        armorDevelopmentBanner.hide();
    }
}
