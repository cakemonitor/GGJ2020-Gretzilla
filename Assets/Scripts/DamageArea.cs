using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        Component[] scripts = other.gameObject.GetComponents<MonoBehaviour>();

        bool smashOccured = false;
        foreach (var script in scripts)
        {
            ISmashable smashable = script as ISmashable;
            if (smashable != null)
            {
                smashable.Smash();
                smashOccured = true;
            }
        }
        if (smashOccured)
        {
            audioSource.Play();
        }
    }
}
