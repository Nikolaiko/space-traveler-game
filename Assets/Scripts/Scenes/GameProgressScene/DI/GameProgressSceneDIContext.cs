using Zenject;

public class GameProgressSceneDIContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container            
            .Bind<ShipBaseParametersCalculator>()
            .To<SimpleShipParametersCalculator>()
            .AsSingle();

        Container            
            .Bind<PlanetsInfoLoader>()
            .To<PlanetsInfoInResourcesLoader>()
            .AsSingle();

        Container
            .Bind<TipsScreenUIManager>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container            
            .Bind<GameProgressUI>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}
