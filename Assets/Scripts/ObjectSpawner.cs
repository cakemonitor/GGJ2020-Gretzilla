using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject planet;
    public float planetRadius = 21.0f;
    public GameObject objectPrefab;
    public int initialAmount = 5;
    public float minSpawnTime = 5.0f;
    public float maxSpawnTime = 15.0f;

    GameObject spawnHelper;

    void Start()
    {
        spawnHelper = new GameObject();
        spawnHelper.name = objectPrefab.name + " spawn helper";

        for (int i = 0; i < initialAmount - 1; i++)
        {
            Spawn();
        }

        TimedSpawn();
    }

    void Spawn()
    {
        spawnHelper.transform.position = planet.transform.position;
        spawnHelper.transform.rotation = Random.rotation;
        spawnHelper.transform.Translate(0, planetRadius, 0);
        
        Instantiate(objectPrefab, spawnHelper.transform.position, spawnHelper.transform.rotation);
    }

    void TimedSpawn()
    {
        Spawn();
        float nextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("TimedSpawn", nextSpawn);
    }
}
