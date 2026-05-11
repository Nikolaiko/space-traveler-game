using Zenject;

public class SofyaMainSceneDIContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<MainSceneUI>()
            .To<SofyaMainSceneUI>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}