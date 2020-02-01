using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour, ISmashable
{
    public void Smash()
    {
        if (Pollution.PollutionBuildings.Contains(gameObject))
        {
            Pollution.PollutionBuildings.Remove(gameObject);
        }
        Destroy(gameObject);
    }
}
