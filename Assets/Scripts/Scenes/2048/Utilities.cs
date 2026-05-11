using System;
using UnityEngine;

public static class Utilities
{
    public static string[,] GetMatrixFromResourcesData()
    {
        string[,] shapes = new string[FoodCollectionConsts.rows, FoodCollectionConsts.columns];

        TextAsset txt = Resources.Load("debugLevel") as TextAsset;
        string level = txt.text;

        string[] lines = level.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
        for (int row = 0; row < FoodCollectionConsts.rows; row++)
        {
            string[] items = lines[row].Split('|');
            for (int column = 0; column < FoodCollectionConsts.columns; column++)
            {
                shapes[row, column] = items[column];
            }
        }
        return shapes;

    }

    public static string showMatrixOnConsole(ItemArray matrix)
    {
        string x = string.Empty;
        for (int row = FoodCollectionConsts.rows - 1; row >= 0; row--)
        {
            for (int column = 0; column < FoodCollectionConsts.columns; column++)
            {
                if (matrix[row, column] != null)
                {
                    x += matrix[row, column].value + "|";
                }
                else
                {
                    x += "X" + "|";
                }
            }
            x += Environment.NewLine;
        }
        Debug.Log(x);
        return x;
    }
}
