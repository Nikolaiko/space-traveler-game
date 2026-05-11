using Zenject;

public class FuelSocobanDIInstaller : MonoInstaller
{
    public override void InstallBindings() {
        Container
            .Bind<SocobanLevelBuilder>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container
            .Bind<SocobanLevelUI>()
            .FromComponentInHierarchy()
            .AsTransient();
        
        Container
            .Bind<TipsScreenUIManager>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}
