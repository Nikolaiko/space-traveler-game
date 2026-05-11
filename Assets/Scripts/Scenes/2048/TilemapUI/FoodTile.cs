using UnityEngine;
using UnityEngine.UI;

public class FoodTile : MonoBehaviour
{
    public Image cartImage;
    public Image valueImage;
    public Sprite[] valueSprites;
    public Sprite[] cartSprites;
    private int value = 0;

    public void setValue(int newValue)
    {
        value = newValue;
        if (value == 0)
        {
            valueImage.gameObject.SetActive(false);
            cartImage.gameObject.SetActive(false);
        } else
        {
            valueImage.sprite = getImageFromValue();
            valueImage.gameObject.SetActive(true);

            cartImage.sprite = getCartImageFromValue(); 
            cartImage.gameObject.SetActive(true);
        }
    }

    public int getValue()
    {
        return value;
    }

    private Sprite getCartImageFromValue()
    {
        switch (value)
        {
            case 2: {
                return cartSprites[0];
            }
            case 4: {
                return cartSprites[0];
            }
            case 8: {
                return cartSprites[1];
            }
            case 16: {
                return cartSprites[1];
            }
            case 32: {
                return cartSprites[1];
            }
            case 64: {
                return cartSprites[2];
            }
            case 128: {
                return cartSprites[2];
            }
            case 256: {
                return cartSprites[3];
            }
            case 512: {
                return cartSprites[3];
            }
            case 1024: {
                return cartSprites[4];
            }
            case 2048: {
                return cartSprites[4];
            }
            default: {
                return cartSprites[0];
            }           
        }
    }

    private Sprite getImageFromValue()
    {
        switch (value)
        {
            case 2: {
                return valueSprites[0];
            }
            case 4: {
                return valueSprites[1];
            }
            case 8: {
                return valueSprites[2];
            }
            case 16: {
                return valueSprites[3];
            }
            case 32: {
                return valueSprites[4];
            }
            case 64: {
                return valueSprites[5];
            }
            case 128: {
                return valueSprites[6];
            }
            case 256: {
                return valueSprites[7];
            }
            case 512: {
                return valueSprites[8];
            }
            case 1024: {
                return valueSprites[9];
            }
            case 2048: {
                return valueSprites[10];
            }
            default: {
                return valueSprites[0];
            }           
        }
    }
}
