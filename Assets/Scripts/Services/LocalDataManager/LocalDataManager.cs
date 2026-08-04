public interface LocalDataManager
{
    SpaceShipState? getSavedState();
    void deleteSavedState();
    void saveGameState(SpaceShipState state);

    UserTipsState getUserTipsState();
    void saveUserTipsState(UserTipsState userTips);

    GameArtType getArtType();
    void saveGameArtType(GameArtType gameArtType);

    void saveUserSettings(UserSettings userSettings);
    UserSettings getUserSettings();
}
