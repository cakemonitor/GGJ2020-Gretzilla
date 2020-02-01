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
        bool spawnSiteFound = false;
        int attemptCount = 0;
        while (!spawnSiteFound && attemptCount < 100)
        {
            spawnHelper.transform.position = planet.transform.position;
            spawnHelper.transform.rotation = Random.rotation;

            Ray ray = new Ray();
            ray.origin = spawnHelper.transform.position + spawnHelper.transform.up * planetRadius * 1.5f;
            ray.direction = planet.transform.position - ray.origin;
            LayerMask mask = LayerMask.GetMask("Landmass");
            if (Physics.Raycast(ray, planetRadius, mask))
            {
                spawnSiteFound = true;
            }
            attemptCount++;
        }

        if (attemptCount >= 90)
            Debug.Log("tried " + attemptCount + " times.");

        spawnHelper.transform.Translate(0, planetRadius, 0);  
        GameObject CurrentSpawn = Instantiate(objectPrefab, spawnHelper.transform.position, spawnHelper.transform.rotation);

        if (!Pollution.PollutionBuildings.Contains(CurrentSpawn))
        {
            Pollution.PollutionBuildings.Add(CurrentSpawn);
        }
        CurrentSpawn = null;
    }

    void TimedSpawn()
    {
        Spawn();
        float nextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("TimedSpawn", nextSpawn);
    }
}
