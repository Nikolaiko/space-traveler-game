using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OlgaPlanetDescriptionUI : MonoBehaviour
{
    public Image bluePlanetDesc;
    public Image redPlanetDesc;
    public Image greenPlanetDesc;
    public Image purplePlanetDesc;

    private Image selectedImage;
    private Dictionary<DestinationPlanetType, Image> planetDescriptions = new Dictionary<DestinationPlanetType, Image>();

    public void Start()
    {
        planetDescriptions.Add(DestinationPlanetType.Green, greenPlanetDesc);
        planetDescriptions.Add(DestinationPlanetType.Blue, bluePlanetDesc);
        planetDescriptions.Add(DestinationPlanetType.Red, redPlanetDesc);
        planetDescriptions.Add(DestinationPlanetType.Purple, purplePlanetDesc);
    }

    public void selectPlanet(DestinationPlanetType planetType)
    {
        if (selectedImage != null)
        {
            selectedImage.gameObject.SetActive(false);
        }

        selectedImage = planetDescriptions[planetType];
        print(selectedImage);
        selectedImage.gameObject.SetActive(true);
    }
}
