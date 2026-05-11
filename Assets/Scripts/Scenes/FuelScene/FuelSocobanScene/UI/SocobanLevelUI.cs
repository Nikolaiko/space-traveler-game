using System;
using TMPro;
using UnityEngine;
using Zenject;

public class SocobanLevelUI: MonoBehaviour {

    [Inject]
    private TipsScreenUIManager tipsScreenManager;

    private int totalBoxCount = 0;
    private int totalCollectedCount = 0;
    
    public Action<GameTipType> onTipScreenClosed;

    public TMP_Text collectedTextField;

    public void Awake() {
        tipsScreenManager.onTipScreenClosed += onCloseTip;
    }

    public void showTip(GameTipType tipType) {
        tipsScreenManager.showTip(tipType);
    }

    public void setTotalBoxCount(int totalBoxCount) {
        this.totalBoxCount = totalBoxCount;
        updateCollectedCount();
    }

    public void setTotalBoxCollected(int totalBoxCollected) {
        totalCollectedCount = totalBoxCollected;
        updateCollectedCount();
    }

    private void onCloseTip(GameTipType tipType) {
        onTipScreenClosed?.Invoke(tipType);
    }

    private void updateCollectedCount() {
        collectedTextField.text = $"Собрано {totalCollectedCount} из {totalBoxCount}";
    }
}