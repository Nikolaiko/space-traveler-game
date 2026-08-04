using UnityEngine;
using Zenject;

public class GameOptionsScreen : MonoBehaviour
{
    public CheckBox checkBox;

    [Inject]
    private SceneLoader sceneLoader;

    [Inject]
    private LocalDataManager localDataManager;

    [Inject]
    private SoundService soundService;

    private UserSettings settings;

    public void Awake() {
        settings = localDataManager.getUserSettings();
        checkBox.setSelected(settings.musicOn);
    }

    public void onQuitGameClick()
    {
        Application.Quit();
    }

    public void onGotoMainMenuClick()
    {
        sceneLoader.loadScene(OlgaSceneNumbers.MAIN_SCENE_ID);
    }

    public void onCloseButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void toggleMusic()
    {
        settings = settings.copy(musicOn: !settings.musicOn);
        checkBox.setSelected(settings.musicOn);
        if (checkBox.selected) {
            soundService.playMusic();
        } else {
            soundService.stopMusic();
        }
        localDataManager.saveUserSettings(settings);
    }
}
