using Zenject;

public class BypassSchemePairsDIContext : MonoInstaller
{
    public override void InstallBindings() {
        Container
            .Bind<BypassSchemeUI>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container
            .Bind<BypassSchemeSidePanel>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container
            .Bind<TipsScreenUIManager>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}
