using System;
using System.Collections.Generic;
using System.Linq;
using ListTypeExtensions;
using UnityEngine;
using Zenject;

public class SchemePairingByPassScene : MonoBehaviour
{
    private static float EASY_TOTAL_SECONDS = 60.0f;
    private static float NORMAL_TOTAL_SECONDS = 60.0f;

    [Inject]
    private BypassSchemeSidePanel sidePanel;

    [Inject]
    private BypassSchemeUI sceneUI;

    [Inject]
    private TipsManager tipsManager;

    private System.Random randomNumberGenerator = new System.Random();
    private FuelScene parentScene;
    private float timeLeft = 0;
    private bool gameStarted = false;
    private BypassButtonState? previousState;
    private BypassSchemePairsButton previousButton;
    private Dictionary<BypassButtonState, bool> schemaState;
    private List<BypassSchemePairsButton> levelButtons;

    public GameObject normalPairs;
    public GameObject hardPairs;

    public void Start() {
        sceneUI.onTipScreenClosed += onTipClosed;
        parentScene = GameObject.FindFirstObjectByType<FuelScene>();

        initGame();

        if (!tipsManager.tipWasShown(GameTipType.bypassSchemaTip)) {            
            sceneUI.showTip(GameTipType.bypassSchemaTip);
        } else {
            startGame();
        }
    }

    public void Update() {
        if (!gameStarted) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {
            parentScene.onResult(false);
        }
        sidePanel.setTimerLeft(timeLeft);
    }

    public void onCancelBypass() {
        parentScene.onResult(false);
    }

    private void initGame() {
        FuelStoreDoorType lastDoorType = parentScene == null ? FuelStoreDoorType.normal : parentScene.lastOpenedDoorType;

        GameObject[] schemaObjects;
        if (lastDoorType == FuelStoreDoorType.normal)
        {
            schemaObjects = GameObject.FindGameObjectsWithTag(GameObjectTags.EASY_PAIR_TAG);
            hardPairs.SetActive(false);
        } else
        {
            schemaObjects = GameObject.FindGameObjectsWithTag(GameObjectTags.NORMAL_PAIR_TAG);
            normalPairs.SetActive(false);
        }

        int schemaIndex = randomNumberGenerator.Next(0, schemaObjects.Length);
        BypassSchemeButtonsContainer container = schemaObjects[schemaIndex].GetComponent<BypassSchemeButtonsContainer>();
        levelButtons = container.buttons.ToList();
        
        int pairsCount = levelButtons.Count / 2;
        List<BypassButtonState> states = Enum.GetValues(typeof(BypassButtonState)).Cast<BypassButtonState>().ToList();
        states.Shuffle(randomNumberGenerator);
        states.RemoveRange(pairsCount, states.Count - pairsCount);

        List<Color> colors = BypassSchemeColors.availableColors.ToList();
        colors.Shuffle(randomNumberGenerator);
        colors.RemoveRange(pairsCount, colors.Count - pairsCount);        

        schemaState = new Dictionary<BypassButtonState, bool>();
        for (int i = 0; i < pairsCount; i++) {            
            schemaState.Add(states[i], false);
            sidePanel.addSchemaPair(states[i]);
        }
        
        levelButtons.Shuffle(randomNumberGenerator);
        
        int colorsCount = 0;
        for (int i = 0; i < levelButtons.Count; i+=2) {
            levelButtons[i].buttonState = states[colorsCount];
            levelButtons[i + 1].buttonState = states[colorsCount];
            colorsCount += 1;
        }

        levelButtons.ForEach(button => {
            button.activate();
            button.clickAction = onButtonClick;            
        });

        timeLeft = lastDoorType == FuelStoreDoorType.normal ? EASY_TOTAL_SECONDS : NORMAL_TOTAL_SECONDS;
    }

    private void startGame() {
        gameStarted = true;
    }

    private void onButtonClick(BypassSchemePairsButton button) {
        BypassButtonState clickedState = button.buttonState;

        if (previousButton == null) {    
            button.openButton();

            previousButton = button;
            previousState = clickedState;
        } else if (previousState == clickedState) {
            button.openButton();
            schemaState[button.buttonState] = true;

            previousState = null;
            previousButton = null;

            sidePanel.unlockPair(button.buttonState);            
            if (sidePanel.allPairsUnlocked()) {
                parentScene.onResult(true);
            }            
        } else {
            previousButton.closeButton();
            previousButton = null;
            previousState = null;

            button.blink();
        }                
    }

    private void onTipClosed(GameTipType tipType) {
        tipsManager.setTipWasShown(tipType, true);
        startGame();
    }
}
