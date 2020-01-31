using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : MonoBehaviour
{

    public float PollutionLevel;
    public float PollutionMultiplier = 0.1f;


    public GameObject Cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PollutionLevel < 100)
        {
            PollutionLevel += (PollutionMultiplier * Time.deltaTime);
        }

        if (Cube.transform.localScale.x != PollutionLevel)
        {
            Cube.transform.localScale = new Vector3(PollutionLevel, Cube.transform.localScale.y, Cube.transform.localScale.z);
        }
    }
}
