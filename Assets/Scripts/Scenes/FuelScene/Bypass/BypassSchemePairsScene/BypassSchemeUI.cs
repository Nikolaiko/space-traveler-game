using System;
using UnityEngine;
using Zenject;

public class BypassSchemeUI: MonoBehaviour {

    [Inject]
    private TipsScreenUIManager tipsScreenManager;

    public Action<GameTipType> onTipScreenClosed;

    public void Awake() {
        tipsScreenManager.onTipScreenClosed += onCloseTip;
    }

    public void showTip(GameTipType tipType) {        
        tipsScreenManager.showTip(tipType);
    }

    private void onCloseTip(GameTipType tipType) {        
        onTipScreenClosed?.Invoke(tipType);
    }
}