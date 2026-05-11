using Assets.Scripts;
using System;
using System.Linq;
using System.Collections.Generic;

public class ItemArray
{
    private System.Random random = new();

    public FoodItem[,] matrix = new FoodItem[
        FoodCollectionConsts.rows,
        FoodCollectionConsts.columns
    ];

    public FoodItem this[int row, int column] {
        get { return matrix[row, column]; }
        set { matrix[row, column] = value; }
    }

    public void getRandomRowColumn(out int row, out int column) {
        do {
            row = random.Next(0, FoodCollectionConsts.rows);
            column = random.Next(0, FoodCollectionConsts.columns);
        } while (matrix[row, column] != null);
    }

    public List<ItemMovementDetails> moveHorizontal(ItemHorizontalMovement horizontalMovement) {
        ResetWasJustDuplicatedValues();

        var movementDetails = new List<ItemMovementDetails>();
        int relativeColumn = horizontalMovement == ItemHorizontalMovement.Left ? -1 : 1;        
        var columnNumbers = Enumerable.Range(0, FoodCollectionConsts.columns);

        if (horizontalMovement == ItemHorizontalMovement.Right) {
            columnNumbers = columnNumbers.Reverse();
        }

        for (int row = FoodCollectionConsts.rows - 1; row >= 0; row--) {
            foreach (int column in columnNumbers) {            
                if (matrix[row, column] == null) continue;

                ItemMovementDetails imd = AreTheseTwoItemsSame(row, column, row, column + relativeColumn);
                if (imd != null) {                    
                    movementDetails.Add(imd);
                    continue;
                }

                int columnFirstNullItem = -1;
                int numberOfItemsToTake = horizontalMovement == ItemHorizontalMovement.Left ? column : FoodCollectionConsts.columns - column;
                bool emptyItemFound = false;

                foreach (var tempColumnFirstNullItem in columnNumbers.Take(numberOfItemsToTake)) {                    
                    columnFirstNullItem = tempColumnFirstNullItem;
                    if (matrix[row, columnFirstNullItem] == null) {
                        emptyItemFound = true;
                        break;
                    }
                }
                
                if (!emptyItemFound) {
                    continue;
                }

                ItemMovementDetails newImd = MoveItemToNullPositionAndCheckIfSameWithNextOne(row, row, row, column, columnFirstNullItem, columnFirstNullItem + relativeColumn);
                movementDetails.Add(newImd);
            }
        }
        return movementDetails;
    }

    public List<ItemMovementDetails> moveVertical(ItemVerticalMovement verticalMovement) {
        ResetWasJustDuplicatedValues();

        var movementDetails = new List<ItemMovementDetails>();

        int relativeRow = verticalMovement == ItemVerticalMovement.Bottom ? -1 : 1;
        var rowNumbers = Enumerable.Range(0, FoodCollectionConsts.rows);

        if (verticalMovement == ItemVerticalMovement.Top) {
            rowNumbers = rowNumbers.Reverse();
        }

        for (int column = 0; column < FoodCollectionConsts.columns; column++) {
            foreach (int row in rowNumbers) {
                //if the item is null, continue checking for non-null items
                if (matrix[row, column] == null) continue;

                //we have a non-null item
                //first we check if this item has the same value as the next one
                ItemMovementDetails imd = AreTheseTwoItemsSame(row, column, row + relativeRow, column);
                if (imd != null) {
                    movementDetails.Add(imd);

                    continue;
                }

                //matrix[row,column] is the first not null item
                //move it to the first null item
                int rowFirstNullItem = -1;

                int numberOfItemsToTake = verticalMovement == ItemVerticalMovement.Bottom ? row : FoodCollectionConsts.rows - row;


                bool emptyItemFound = false;

                foreach (var tempRowFirstNullItem in rowNumbers.Take(numberOfItemsToTake)) {
                    rowFirstNullItem = tempRowFirstNullItem;
                    if (matrix[rowFirstNullItem, column] == null) {
                        emptyItemFound = true;
                        break;
                    }
                }

                if (!emptyItemFound) {
                    continue;
                }

                ItemMovementDetails newImd = MoveItemToNullPositionAndCheckIfSameWithNextOne(row, rowFirstNullItem, rowFirstNullItem + relativeRow, column, column, column);

                movementDetails.Add(newImd);
            }
        }
        return movementDetails;
    }

    public FoodCollectionGamePoleState getGamePoleState() {
        int gamePoleValue = 0;
        int valueCellsCount = 0;

        for (int row = 0; row < FoodCollectionConsts.rows; row++) {
            for (int column = 0; column < FoodCollectionConsts.columns; column++) {
                if (matrix[row, column] != null) {
                    if (gamePoleValue < matrix[row, column].value) {
                        gamePoleValue = matrix[row, column].value;                        
                    }                    
                    valueCellsCount += 1;
                }
            }
        }
        return new FoodCollectionGamePoleState(gamePoleValue, valueCellsCount);
    }
    private ItemMovementDetails MoveItemToNullPositionAndCheckIfSameWithNextOne(
        int oldRow,
        int newRow,
        int itemToCheckRow,
        int oldColumn,
        int newColumn,
        int itemToCheckColumn
    ) {
        //we found a null item, so we attempt the switch ;)
        //bring the first not null item to the position of the first null one
        matrix[newRow, newColumn] = matrix[oldRow, oldColumn];
        matrix[oldRow, oldColumn] = null;

        //check if we have the same value as the left one
        ItemMovementDetails imd2 = AreTheseTwoItemsSame(newRow, newColumn, itemToCheckRow, itemToCheckColumn);
        if (imd2 != null) {
            //we have, so add the item returned by the method
            return imd2;
        }
        //they are not the same, so we'll just animate the current item to its new position
        return new ItemMovementDetails(newRow, newColumn);
    }

    private ItemMovementDetails AreTheseTwoItemsSame(
        int originalRow,
        int originalColumn,
        int toCheckRow,
        int toCheckColumn
    ) {
        if (toCheckRow < 0 || toCheckColumn < 0 || toCheckRow >= FoodCollectionConsts.rows || toCheckColumn >= FoodCollectionConsts.columns) {
            return null;
        }

        if (
            matrix[originalRow, originalColumn] != null && matrix[toCheckRow, toCheckColumn] != null
            && matrix[originalRow, originalColumn].value == matrix[toCheckRow, toCheckColumn].value
            && !matrix[toCheckRow, toCheckColumn].wasJustDuplicated
        ) {            
            matrix[toCheckRow, toCheckColumn].value *= 2;
            matrix[toCheckRow, toCheckColumn].wasJustDuplicated = true;                        
            matrix[originalRow, originalColumn] = null;
            return new ItemMovementDetails(toCheckRow, toCheckColumn);
        } else {
            return null;
        }
    }

    private void ResetWasJustDuplicatedValues() {
        for (int row = 0; row < FoodCollectionConsts.rows; row++) {
            for (int column = 0; column < FoodCollectionConsts.columns; column++) {
                if (matrix[row, column] != null && matrix[row, column].wasJustDuplicated) {
                    matrix[row, column].wasJustDuplicated = false;
                }
            }
        }
    }
}
