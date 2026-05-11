public class OlgaSceneNumbers
{
    public static int MAIN_SCENE_ID = 0;
    public static int STORY_SCENE_ID = 1;
    public static int GAME_PROGRESS_SCENE_ID = 2;
    public static int FUEL_SCENE_ID = 3;
    public static int FUEL_SOCOBAN_SCENE_ID = 4;
    public static int SCENE_2048_ID = 5;
    public static int SUCCESS_SCENE_ID = 7;
    public static int FAIL_SCENE_ID = 6;
    public static int SELECT_PLANET_SCENE_ID = 8;
    public static int BYPASS_HACKING_SCENE_ID = 9;
    public static int BYPASS_SCHEME_PAIRS_SCENE_ID = 10;

    public static int sceneNumberFromSceneType(GameSceneType sceneType)
    {
        switch (sceneType)
        {
            case GameSceneType.main:
                return MAIN_SCENE_ID;
            case GameSceneType.story:
                return STORY_SCENE_ID;
            case GameSceneType.planeSelect:
                return SELECT_PLANET_SCENE_ID;
            case GameSceneType.gameProgress:
                return GAME_PROGRESS_SCENE_ID;
            case GameSceneType.food:
                return SCENE_2048_ID;
            case GameSceneType.fuel:
                return FUEL_SCENE_ID;
            case GameSceneType.fuelSocoban:
                return FUEL_SOCOBAN_SCENE_ID;
            case GameSceneType.bypassPairs:
                return BYPASS_SCHEME_PAIRS_SCENE_ID;
            case GameSceneType.success:
                return SUCCESS_SCENE_ID;
            case GameSceneType.fail:
                return FAIL_SCENE_ID;
            default:
                return -1;
        }
    }
}
