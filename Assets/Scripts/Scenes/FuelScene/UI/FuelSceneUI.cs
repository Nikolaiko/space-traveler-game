using System;
using TMPro;
using UnityEngine;
using Zenject;

public class FuelSceneUI : MonoBehaviour
{
    [Inject]
    private TipsScreenUIManager tipsScreenManager;
    
    public Action<GameTipType> onTipScreenClosed;

    public TMP_Text timeLeftText;
    public TMP_Text fuelNeededText;
    public TMP_Text fuelCollectedText;

    public Action onWinScreenClose;
    public Action onLooseScreenClose;

    public SingleButtonScreen winScreen;
    public SingleButtonScreen looseScreen;
    
    public void Start() {
        tipsScreenManager.onTipScreenClosed += onCloseTip;
        winScreen.onClick = onWinScreenClose;
        looseScreen.onClick = onLooseScreenClose;
    }

    public void showTip(GameTipType tipType) {
        hideUI();
        tipsScreenManager.showTip(tipType);
    }

    public void updateTimeLeft(float timeLeft) {
        float timeSpanConversiondMinutes = TimeSpan.FromSeconds(timeLeft).Minutes;
        float timeSpanConversionSeconds = TimeSpan.FromSeconds(timeLeft).Seconds;

        timeLeftText.text = $"Время: {timeSpanConversiondMinutes}:{timeSpanConversionSeconds}";
    }

    public void updateCollectedFuel(int totalFuel) {
        fuelCollectedText.text = $"Собрано: {totalFuel}";
    }

    public void updateNeededFuel(int fuelValue) {
        fuelNeededText.text = $"Нужно: {fuelValue}";
    }

    public void hideUI() {
        fuelCollectedText.gameObject.SetActive(false);
        fuelNeededText.gameObject.SetActive(false);
        timeLeftText.gameObject.SetActive(false);        
    }

    public void showUI() {
        fuelCollectedText.gameObject.SetActive(true);
        fuelNeededText.gameObject.SetActive(true);
        timeLeftText.gameObject.SetActive(true);        
    }

    private void onCloseTip(GameTipType tipType) {
        showUI();
        onTipScreenClosed?.Invoke(tipType);
    }

    public void showWinScreen()
    {
        winScreen.showScreen();
    }

    public void showLooseScreen()
    {
        looseScreen.showScreen();
    }
}
