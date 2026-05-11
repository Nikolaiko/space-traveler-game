using Zenject;

public class OlgaSelectPlanetDIContext : MonoInstaller
{
    public override void InstallBindings() {
        Container            
            .Bind<SelectPlanetSceneUI>()
            .To<OlgaSelectPlanetUI>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container            
            .Bind<PlanetsInfoLoader>()
            .To<PlanetsInfoInResourcesLoader>()
            .AsTransient();

        Container            
            .Bind<ShipBaseParametersCalculator>()
            .To<SimpleShipParametersCalculator>()
            .AsTransient();
    }
}
