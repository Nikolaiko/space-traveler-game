using UnityEngine;

public class SelectedPlanedObject : MonoBehaviour
{
    public OlgaPlanetObjectUI redPlanet;
    public OlgaPlanetObjectUI greenPlanet;
    public OlgaPlanetObjectUI bluePlanet;
    public OlgaPlanetObjectUI purplePlanet;

    public void setSelectedPlanet(DestinationPlanetType planetType)
    {
        switch (planetType)
        {
            case DestinationPlanetType.Red:
                redPlanet.gameObject.SetActive(true);
                redPlanet.setSelected(true);
                break;
            case DestinationPlanetType.Green:
                greenPlanet.gameObject.SetActive(true);
                greenPlanet.setSelected(true);
                break;
            case DestinationPlanetType.Blue:
                bluePlanet.gameObject.SetActive(true);
                bluePlanet.setSelected(true);
                break;
            case DestinationPlanetType.Purple:
                purplePlanet.gameObject.SetActive(true);
                purplePlanet.setSelected(true);
                break;
        }
    }
}
