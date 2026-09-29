using Zenject;

public class ProjectDIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<LocalDataManager>()
            .To<UserPrefsManager>()
            .AsSingle();

        Container
            .Bind<ParametersService>()
            .To<GameParametersService>() 
            .AsSingle();

#if UNITY_EDITOR
        Container
            .Bind<TipsManager>()
            .To<DebugTipsManager>()
            .AsSingle();
#else
        Container
            .Bind<TipsManager>()
            .To<GameTipsManager>()
            .AsSingle();
#endif

        Container
            .Bind<SceneLoader>()
            .To<UnitySceneLoader>()
            .AsTransient();

        Container
            .Bind<TipsScreenUIFactory>()
            .FromComponentInNewPrefabResource("Prefabs/TipsScreenUIFactory")
            .AsTransient();

        Container
            .Bind<SoundService>()
            .FromComponentInNewPrefabResource("Prefabs/SoundService")
            .AsSingle();
    }
}
