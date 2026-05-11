using Zenject;

public class FoodCollectMiniGameDIContext : MonoInstaller
{
    public override void InstallBindings() {
        Container
            .Bind<FoodTileManager>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container
            .Bind<FoodCollectUI>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container
            .Bind<TipsScreenUIManager>()
            .FromComponentInHierarchy()
            .AsTransient();
    }
}

