using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Component[] scripts = other.gameObject.GetComponents<MonoBehaviour>();

        foreach (var script in scripts)
        {
            ISmashable smashable = script as ISmashable;
            if (smashable != null)
                smashable.Smash();
        }
    }    
}
