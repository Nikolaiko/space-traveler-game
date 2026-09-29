using Zenject;

public class SelectPlanetDIContext : MonoInstaller
{
    public override void InstallBindings() {
        Container            
            .Bind<SelectPlanetSceneUI>()
            .To<SelectPlanetUI>()
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
