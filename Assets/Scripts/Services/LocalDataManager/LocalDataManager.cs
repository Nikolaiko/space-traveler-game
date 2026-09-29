public interface LocalDataManager
{
    SpaceShipState? getSavedState();
    void deleteSavedState();
    void saveGameState(SpaceShipState state);

    UserTipsState getUserTipsState();
    void saveUserTipsState(UserTipsState userTips);

    void saveUserSettings(UserSettings userSettings);
    UserSettings getUserSettings();
}
