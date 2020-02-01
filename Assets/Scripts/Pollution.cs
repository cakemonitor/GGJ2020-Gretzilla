using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pollution : MonoBehaviour
{

    public float PollutionLevel = 500;
    public float PollutionMultiplier = 1f;

    public Image LoseScreen;

    // Update is called once per frame
    void Update()
    {
        if (PollutionLevel > 100)
        {
            PollutionLevel -= (PollutionMultiplier * Time.deltaTime);
        }
        else
        {
            if (LoseScreen != null)
            {
                if (LoseScreen.enabled == false)
                {
                    LoseScreen.enabled = true;
                }
            }
        }
    }
}
