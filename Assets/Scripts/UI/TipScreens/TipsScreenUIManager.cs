using System;
using UnityEngine;
using Zenject;

public class TipsScreenUIManager : MonoBehaviour
{
    [Inject]
    private TipsScreenUIFactory tipsScreenUIFactory;

    public TipsScreenUI currentTipScreen;

    public Action<GameTipType> onTipScreenClosed;

    public void showTip(GameTipType tipType) {
        currentTipScreen = tipsScreenUIFactory.buildTipScreen(tipType);
        currentTipScreen.setParent(transform);
        currentTipScreen.onCloseScreen = closeTipScreen;
    }

    private void closeTipScreen() {
        onTipScreenClosed(currentTipScreen.type);
        currentTipScreen.removeFromParent();
    }
}
