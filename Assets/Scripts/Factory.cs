using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour, ISmashable
{
    public void Smash()
    {
        Destroy(gameObject);
    }
}
