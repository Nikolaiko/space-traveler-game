public interface SelectPlanetSceneUI
{
    public delegate void PlanetSelectDelegate(DestinationPlanetType planetType);
    public event PlanetSelectDelegate onPlanetSelect;

    public delegate void StartButtonClickDelegate();
    public event StartButtonClickDelegate onStartButtonClick;

    void setSelectedPlanetStats(int distance, int obstacles);
}
