using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StoryUIWithMultipleEndings : StoryWithTextUIScreen
{
    [Inject]
    private LocalDataManager localDataManager;
    private DestinationPlanetType selectedPlanet;
    public Sprite[] endImages;
    
    public void Awake()
    {
        if (localDataManager.getSavedState().HasValue) {
            selectedPlanet = localDataManager.getSavedState().Value.planetType;
        }

        int index = (int)selectedPlanet;
        try {
            comixFrames = comixFrames.Append(endImages[index]).ToArray();
        } catch (IndexOutOfRangeException exception) {
            comixFrames = comixFrames.Append(endImages[0]).ToArray();
            Debug.LogError("On index: " + index + ". Exception: " + exception);
        }
    }
}
