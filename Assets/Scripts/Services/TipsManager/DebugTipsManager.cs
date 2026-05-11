public class DebugTipsManager : TipsManager
{
    public void setTipWasShown(GameTipType tipType, bool wasShown) {}

    public bool tipWasShown(GameTipType tipType) {
        return false;
    }
}