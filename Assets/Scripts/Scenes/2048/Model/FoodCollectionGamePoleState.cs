using System.ComponentModel;

[ImmutableObject(true)]
public struct FoodCollectionGamePoleState
{
    public FoodCollectionGamePoleState(
        int gamePoleScore,
        int valueCellsCount
    ) {
        this.gamePoleScore = gamePoleScore;
        this.valueCellsCount = valueCellsCount;        
    }

    public readonly int gamePoleScore;
    public readonly int valueCellsCount;
}
