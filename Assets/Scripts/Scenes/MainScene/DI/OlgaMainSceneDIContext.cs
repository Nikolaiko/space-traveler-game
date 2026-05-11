using Zenject;

public class OlgaMainSceneDIContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<MainSceneUI>()
            .To<OlgaMainScreenUI>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}
