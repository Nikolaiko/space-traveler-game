using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;
using Zenject;
using TMPro;

public class GameManager : MonoBehaviour
{
    private readonly float distance = 1.0f;
    private readonly int zIndex = 0;

    [Inject]
    private SceneLoader sceneLoader;

    [Inject]
    private LocalDataManager localDataManager;

    [Inject]
    private FoodTileManager tileManager;

    [Inject]
    private TipsManager tipsManager;

    [Inject]
    private FoodCollectUI sceneUI;

    private GameStateManager spaceShipProgress = new GameStateManager();
    private IInputDetector inputDetector;
    private FoodCollectionGameState gameState = FoodCollectionGameState.Playing;
    private ItemArray matrix;
    private int foodNeeded = 0;
    private int turnsCount = 0;

    public SingleButtonScreen winScreen;
    public SingleButtonScreen looseScreen;
    public TextMeshProUGUI NeededFoodText;    
    
    public void Start() {
        sceneUI.onTipScreenClosed += onTipClosed;

        initialize();

        inputDetector = GetComponent<IInputDetector>();

        string x = Utilities.showMatrixOnConsole(matrix);

        winScreen.onClick = winGame;
        looseScreen.onClick = looseGame;

        if (!tipsManager.tipWasShown(GameTipType.foodCollectionTip)) {            
            sceneUI.showTip(GameTipType.foodCollectionTip);
        }     
    }

    public void looseGame() {
        sceneLoader.loadScene(OlgaSceneNumbers.GAME_PROGRESS_SCENE_ID);
    }

    private void winGame() {
        sceneLoader.loadScene(OlgaSceneNumbers.GAME_PROGRESS_SCENE_ID);
    }

    private void completeGame(FoodCollectionGamePoleState gamePoleState) {
        SpaceShipState? shipState = localDataManager.getSavedState();
        if (shipState.HasValue) {
            SpaceShipState newState = shipState.Value.copy(
                foodCollected: gamePoleState.gamePoleScore,
                foodWeight: gamePoleState.valueCellsCount * FoodCollectionConsts.foodBoxWeight
            );
            localDataManager.saveGameState(newState);
        }        

        gameState = FoodCollectionGameState.Won;
        winScreen.showScreen();
    }

    private void createNewItem(int value = 2, int? row = null, int? column = null) {
        int randomRow, randomColumn;

        if (row == null && column == null) {
            matrix.getRandomRowColumn(out randomRow, out randomColumn);
        } else {
            randomRow = row.Value;
            randomColumn = column.Value;
        }

        var newItem = new FoodItem {
            row = randomRow,
            column = randomColumn,
            value = value,            
        };

        matrix[randomRow, randomColumn] = newItem;
    }

    public void Update() {
        if (gameState == FoodCollectionGameState.Playing) {
            InputDirection? value = inputDetector.DetectInputDirection();

            if (value.HasValue) {
                turnsCount += 1;

                List<ItemMovementDetails> movementDetails = new();                
                if (value == InputDirection.Left)
                    movementDetails = matrix.moveHorizontal(ItemHorizontalMovement.Left);
                else if (value == InputDirection.Right)
                    movementDetails = matrix.moveHorizontal(ItemHorizontalMovement.Right);
                else if (value == InputDirection.Top)
                    movementDetails = matrix.moveVertical(ItemVerticalMovement.Top);
                else if (value == InputDirection.Bottom)
                    movementDetails = matrix.moveVertical(ItemVerticalMovement.Bottom);


                if (movementDetails.Count > 0) {
                    AnimateItems(movementDetails);                    
                }                
            }
        }
    }

    void AnimateItems(IEnumerable<ItemMovementDetails> movementDetails) {
        createNewItem();

        FoodCollectionGamePoleState gamePoleState = matrix.getGamePoleState();
        if (gamePoleState.gamePoleScore >= foodNeeded) {
            completeGame(gamePoleState);            
        } else {
            tileManager.applyMatrix(matrix.matrix);
        }
    }

    private void initialize() {
        matrix = new ItemArray();

        foodNeeded = localDataManager.getSavedState()?.foodNeeded ?? 0;
        NeededFoodText.text = foodNeeded.ToString();

        createNewItem();
        createNewItem();

        tileManager.applyMatrix(matrix.matrix);
        gameState = FoodCollectionGameState.Playing;
    }

    private void onTipClosed(GameTipType tipType) {
        tipsManager.setTipWasShown(tipType, true);
    }
}
