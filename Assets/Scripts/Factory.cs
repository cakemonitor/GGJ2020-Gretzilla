using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour, ISmashable
{
    private Pollution PollutionScript;
    private GameObject Explosion;

    void Awake()
    {
        PollutionScript = (Pollution)FindObjectOfType(typeof(Pollution));
        Explosion = PollutionScript.EffectsToSpawn[0];
    }

    public void Smash()
    {
        Pollution.PollutionBuildings.Remove(gameObject);
        GameObject TempExplosion = Instantiate(Explosion, gameObject.transform);
        TempExplosion.transform.localPosition = new Vector3(0, 4f, 0);
        TempExplosion.transform.localScale = new Vector3(7, 7, 7);
        TempExplosion.transform.parent = null;
        Destroy(gameObject);
    }
}
