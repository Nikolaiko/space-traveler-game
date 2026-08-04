using System;
using Newtonsoft.Json;
using UnityEngine;

public class UserPrefsManager : LocalDataManager
{
    private static string gameStateKeyName = "space_quest_state";
    private static string tipsStateKeyName = "space_quest_tips";
    private static string artTypeKeyName = "art_type";
    private static string userSettingsKeyName = "user_settings";

    public GameArtType getArtType() {
        string json = PlayerPrefs.GetString(artTypeKeyName);
        GameArtType loadedArtType = GameArtType.olgaArt;

        try
        {
            loadedArtType = (GameArtType)Enum.Parse(typeof(GameArtType), json);
        }
        catch (ArgumentException exception)
        {
            Debug.Log(exception);
        }
        catch (FormatException exception)
        {
            Debug.LogException(exception);
        }
        catch (InvalidOperationException exception)
        {
            Debug.LogException(exception);
        }
        return loadedArtType;
    }

    public SpaceShipState? getSavedState() {
        string json = PlayerPrefs.GetString(gameStateKeyName);
        SpaceShipState? state;

        try {
            state = JsonConvert.DeserializeObject<SpaceShipState>(json);
        } catch (ArgumentException) {
            state = null;
        } catch (NullReferenceException) {
            state = null;
        } catch (JsonSerializationException) {
            state = null;
        }
        return state;
    }

    public void deleteSavedState()
    {
        PlayerPrefs.DeleteKey(gameStateKeyName);
    }

    public UserTipsState getUserTipsState() {
        string json = PlayerPrefs.GetString(tipsStateKeyName);
        UserTipsState state = new UserTipsState();

        try {
            state = JsonConvert.DeserializeObject<UserTipsState>(json);
        } catch (ArgumentException) {
            Debug.Log("ArgumentException exception");            
        } catch (NullReferenceException) {
            Debug.Log("NullReferenceException exception");            
        } catch (JsonSerializationException) {
            Debug.Log("JsonSerializationException exception");            
        }
        finally { saveUserTipsState(state); }
        return state;
    }

    public UserSettings getUserSettings() {
        string json = PlayerPrefs.GetString(userSettingsKeyName);
        UserSettings state = new UserSettings();

        try {
            state = JsonConvert.DeserializeObject<UserSettings>(json);
        } catch (ArgumentException) {
            Debug.Log("ArgumentException exception");            
        } catch (NullReferenceException) {
            Debug.Log("NullReferenceException exception");            
        } catch (JsonSerializationException) {
            Debug.Log("JsonSerializationException exception");            
        }
        finally { saveUserSettings(state); }
        return state;
    }

    public void saveUserSettings(UserSettings userSettings) {
        string json = JsonConvert.SerializeObject(userSettings);
        PlayerPrefs.SetString(userSettingsKeyName, json);
    }
    

    public void saveGameArtType(GameArtType gameArtType) {
        string json = gameArtType.ToString();
        PlayerPrefs.SetString(artTypeKeyName, json);
    }

    public void saveGameState(SpaceShipState state) {        
        string json = JsonConvert.SerializeObject(state);
        PlayerPrefs.SetString(gameStateKeyName, json);
    }

    public void saveUserTipsState(UserTipsState userTips) {
        string json = JsonConvert.SerializeObject(userTips);  
        PlayerPrefs.SetString(tipsStateKeyName, json);
    }
}
