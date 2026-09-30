using Zenject;

public class ArmorSceneDIInstaller : MonoInstaller
{
    public override void InstallBindings() {
        Container
            .Bind<ArmorSceneUI>()
            .To<ArmorUI>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}
