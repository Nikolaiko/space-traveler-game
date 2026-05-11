using System.Drawing;
using UnityEngine;

public class GameUIParametersService : UIParametersService
{
    public Size getScreenSize()
    {
        return new Size(Screen.currentResolution.width, Screen.currentResolution.height);
    }
}
