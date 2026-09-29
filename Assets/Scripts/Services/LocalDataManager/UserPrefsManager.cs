using Newtonsoft.Json;
using UnityEngine;

public class UserPrefsManager : LocalDataManager
{
    private static string gameStateKeyName = "space_quest_state";
    private static string tipsStateKeyName = "space_quest_tips";
    private static string userSettingsKeyName = "user_settings";

    public SpaceShipState? getSavedState() {
        return loadJson<SpaceShipState>(gameStateKeyName);
    }

    public void deleteSavedState()
    {
        PlayerPrefs.DeleteKey(gameStateKeyName);
        PlayerPrefs.Save();
    }

    public UserTipsState getUserTipsState() {
        return loadJson<UserTipsState>(tipsStateKeyName) ?? new UserTipsState();
    }

    public UserSettings getUserSettings() {
        return loadJson<UserSettings>(userSettingsKeyName) ?? new UserSettings();
    }

    public void saveUserSettings(UserSettings userSettings) {
        saveJson(userSettingsKeyName, userSettings);
    }

    public void saveGameState(SpaceShipState state) {
        saveJson(gameStateKeyName, state);
    }

    public void saveUserTipsState(UserTipsState userTips) {
        saveJson(tipsStateKeyName, userTips);
    }

    private T? loadJson<T>(string keyName) where T : struct {
        if (!PlayerPrefs.HasKey(keyName)) {
            return null;
        }

        try {
            return JsonConvert.DeserializeObject<T?>(PlayerPrefs.GetString(keyName));
        } catch (JsonException exception) {
            Debug.LogException(exception);
            return null;
        }
    }

    // PlayerPrefs пишет на диск только при штатном выходе, поэтому сохраняем сразу
    private void saveJson(string keyName, object value) {
        PlayerPrefs.SetString(keyName, JsonConvert.SerializeObject(value));
        PlayerPrefs.Save();
    }
}
