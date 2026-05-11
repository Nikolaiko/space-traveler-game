using UnityEngine;
using UnityEngine.UI;

public class OlgaSelectPlanetUI : MonoBehaviour, SelectPlanetSceneUI
{
    public event SelectPlanetSceneUI.PlanetSelectDelegate onPlanetSelect;
    public event SelectPlanetSceneUI.StartButtonClickDelegate onStartButtonClick;

    public OlgaPlanetDescriptionUI planetDescriptionUI;

    public OlgaPlanetObjectUI redPlanet;
    public OlgaPlanetObjectUI bluePlanet;
    public OlgaPlanetObjectUI greenPlanet;
    public OlgaPlanetObjectUI purplePlanet;

    public Button startButton;

    private OlgaPlanetObjectUI selectedPlanet;

    public void Start()
    {
        redPlanet.onPlanetClick += onRedPlanetClick;
        bluePlanet.onPlanetClick += onBluePlanetClick;
        greenPlanet.onPlanetClick += onGreenPlanetClick;
        purplePlanet.onPlanetClick += onPurplePlanetClick;

        startButton.interactable = false;
    }

    public void setSelectedPlanetStats(int distance, int obstacles)
    {
        startButton.interactable = true;
    }

    public void onButtonClick()
    {
        onStartButtonClick?.Invoke();
    }

    private void onRedPlanetClick()
    {
        if (selectedPlanet == redPlanet) return;
        startButton.interactable = true;

        if (selectedPlanet != null)
        {
            selectedPlanet.setSelected(false);
        }
        redPlanet.setSelected(true);
        selectedPlanet = redPlanet;

        planetDescriptionUI.selectPlanet(DestinationPlanetType.Red);
        onPlanetSelect?.Invoke(DestinationPlanetType.Red);
    }

    private void onBluePlanetClick()
    {
        if (selectedPlanet == bluePlanet) return;
        startButton.interactable = true;

        if (selectedPlanet != null)
        {
            selectedPlanet.setSelected(false);
        }
        bluePlanet.setSelected(true);
        selectedPlanet = bluePlanet;

        planetDescriptionUI.selectPlanet(DestinationPlanetType.Blue);
        onPlanetSelect?.Invoke(DestinationPlanetType.Blue);
    }

    private void onGreenPlanetClick()
    {
        if (selectedPlanet == greenPlanet) return;
        startButton.interactable = true;

        if (selectedPlanet != null)
        {
            selectedPlanet.setSelected(false);
        }
        greenPlanet.setSelected(true);
        selectedPlanet = greenPlanet;

        planetDescriptionUI.selectPlanet(DestinationPlanetType.Green);
        onPlanetSelect?.Invoke(DestinationPlanetType.Green);
    }
    
    private void onPurplePlanetClick()
    {
        if (selectedPlanet == purplePlanet) return;
        startButton.interactable = true;

        if (selectedPlanet != null)
        {
            selectedPlanet.setSelected(false);
        }
        purplePlanet.setSelected(true);
        selectedPlanet = purplePlanet;

        planetDescriptionUI.selectPlanet(DestinationPlanetType.Purple);
        onPlanetSelect?.Invoke(DestinationPlanetType.Purple);
    }
}
