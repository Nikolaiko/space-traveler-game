using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlanetsInfoInResourcesLoader : PlanetsInfoLoader
{
    private static string resourceFileName = "PlanetsInfo";

    private List<DestinationPlanetInfo> planetInfos = new List<DestinationPlanetInfo>();

    PlanetsInfoInResourcesLoader() {
        TextAsset text = (TextAsset)Resources.Load(resourceFileName);
        try {            
            planetInfos = JsonConvert.DeserializeObject<List<DestinationPlanetInfo>>(text.text);
        } catch(NullReferenceException nullException) {
            Debug.LogException(nullException);
        } catch(ArgumentNullException nullException) {
            Debug.LogException(nullException);
        } catch(JsonSerializationException serializationException) {
            Debug.LogException(serializationException);
        }
    }

    public List<DestinationPlanetInfo> loadPlanetsInfo() {
        return planetInfos;
    }
}