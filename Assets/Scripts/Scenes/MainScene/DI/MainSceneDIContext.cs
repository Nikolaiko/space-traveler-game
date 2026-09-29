using Zenject;

public class MainSceneDIContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<MainSceneUI>()
            .To<MainMenuScreenUI>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}
