public interface TipsManager
{
    bool tipWasShown(GameTipType tipType);
    void setTipWasShown(GameTipType tipType, bool wasShown);
}
