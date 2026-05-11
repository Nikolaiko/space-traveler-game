using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using TMPro;
using UnityEngine;

public class BypassSchemeSidePanel : MonoBehaviour {
    public GameObject rowInstance;

    public TMP_Text timerText;
    public GameObject rowContainer;

    private Dictionary<BypassButtonState, BypassScemePairRow> schemPairRows = new Dictionary<BypassButtonState, BypassScemePairRow>();

    public void setTimerLeft(float timeLeft) {
        double flooredSeconds = Math.Floor(timeLeft);
        string secondsLeft = timeLeft < 10.0 ? "0" + flooredSeconds :  flooredSeconds.ToString();
        timerText.text = "00:" + secondsLeft;
    }

    public void addSchemaPair(BypassButtonState buttonState) {
        GameObject newRow = Instantiate(rowInstance);
        BypassScemePairRow row = newRow.GetComponent<BypassScemePairRow>();

        schemPairRows.Add(buttonState, row);

        row.setStatus(BypassPairStatus.locked);
        newRow.transform.SetParent(rowContainer.transform, false);
    }

    public void unlockPair(BypassButtonState buttonState) {
        schemPairRows[buttonState].setStatus(BypassPairStatus.unlocked);
    }

    public bool allPairsUnlocked() {        
        return schemPairRows.Values.Where(row => row.getStatus() == BypassPairStatus.locked).IsEmpty();
    }
}