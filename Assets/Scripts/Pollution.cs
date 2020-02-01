using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pollution : MonoBehaviour
{
    public static List<GameObject> PollutionBuildings = new List<GameObject>();
    public static List<GameObject> CleanBuildings = new List<GameObject>();

    public float PollutionLevel = 500;
    public float PollutionMultiplier = 1f;
    [Range(0,1)] public float cleanEnergyWinRatio = 0.8f;

    public Image LoseScreen;
    public Image WinScreen;

    void Update()
    {
        if (PollutionLevel > 100)
        {
            PollutionMultiplier = PollutionBuildings.Count - CleanBuildings.Count;
            PollutionLevel -= (PollutionMultiplier * Time.deltaTime);
        }
        else if (LoseScreen != null)
        {
            LoseScreen.enabled = true;
        }

        float cleanRatio = CleanBuildings.Count / (float)(CleanBuildings.Count + PollutionBuildings.Count);
        if (cleanRatio >= cleanEnergyWinRatio)
        {
            WinScreen.enabled = true;
        }
    }
}
