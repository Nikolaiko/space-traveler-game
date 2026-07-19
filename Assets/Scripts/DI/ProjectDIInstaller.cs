using Zenject;

public class ProjectDIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<LocalDataManager>()
            .To<UserPrefsManager>()
            .AsSingle();

        // Container            
        //     .Bind<TipsManager>()
        //     .To<GameTipsManager>() 
        //     .AsSingle();

        Container
            .Bind<ParametersService>()
            .To<GameParametersService>() 
            .AsSingle();

        Container
            .Bind<TipsManager>()
            .To<DebugTipsManager>() 
            .AsSingle();

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
