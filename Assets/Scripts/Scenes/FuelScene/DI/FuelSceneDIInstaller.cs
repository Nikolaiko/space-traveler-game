using Zenject;

public class FuelSceneDIInstaller : MonoInstaller
{
    public override void InstallBindings() {
        Container
            .Bind<FuelSceneUI>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container
            .Bind<TipsScreenUIManager>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}
