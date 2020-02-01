using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFarm : MonoBehaviour, ISmashable
{
    public void Smash()
    {
        Pollution.CleanBuildings.Remove(gameObject);
        Destroy(gameObject);
    }
}
