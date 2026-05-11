using UnityEngine;

public class FoodTileManager : MonoBehaviour
{
    public FoodTile[] foodTiles;

    public void applyMatrix(FoodItem[,] matrix) {        
        for (int i = 0; i < FoodCollectionConsts.rows; i++) {
            for (int j = 0; j < FoodCollectionConsts.columns; j++) {
                FoodItem currentItem = matrix[i, j];
                int row = (FoodCollectionConsts.rows - 1) - i;
                if (currentItem == null) {                    
                    foodTiles[row * FoodCollectionConsts.columns + j].setValue(0);                    
                } else {
                    foodTiles[row * FoodCollectionConsts.columns + j].setValue(currentItem.value);
                }
            }
        }
    }
}
