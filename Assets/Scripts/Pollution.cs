using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : MonoBehaviour
{

    public float PollutionLevel = 500;
    public float PollutionMultiplier = 1f;



    // Update is called once per frame
    void Update()
    {
        if (PollutionLevel > 100)
        {
            PollutionLevel -= (PollutionMultiplier * Time.deltaTime);
        }
        else
        {

        }
    }
}
