using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject planet;
    public float planetRadius = 21.0f;
    public GameObject[] fossilFuelPrefabs;
    public GameObject[] greenEnergyPrefabs;
    public GameObject[] flyingPrefabs;
    public int initialAmount = 5;
    public int fossilFuelQueueSize = 20;
    [Range(0,1)] public float postQueueChanceOfCleanEnergy = 0.5f;
    public float minTimeBetweenSpawns = 5.0f;
    public float maxTimeBetweenSpawns = 15.0f;
    public float minTimeBetweenFlyingSpawns = 5.0f;
    public float maxTimeBetweenFlyingSpawns = 15.0f;

    GameObject spawnHelper;

    void Start()
    {
        spawnHelper = new GameObject();
        spawnHelper.name = "Spawn helper";

        for (int i = 0; i < initialAmount - 1; i++)
        {
            Spawn();
        }

        TimedSpawn();
        TimedFlyingSpawn();
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
        
        GameObject prefab = fossilFuelPrefabs[Random.Range(0, fossilFuelPrefabs.Length)];
        bool pollutiionBuilding = true;
        if (fossilFuelQueueSize > 0)
            fossilFuelQueueSize--;
        else if(Random.value < postQueueChanceOfCleanEnergy)
        {
            prefab = greenEnergyPrefabs[Random.Range(0, greenEnergyPrefabs.Length)];
            pollutiionBuilding = false;
        }

        GameObject CurrentSpawn = Instantiate(prefab, spawnHelper.transform.position, spawnHelper.transform.rotation);

        if (pollutiionBuilding)
            Pollution.PollutionBuildings.Add(CurrentSpawn);
        else
            Pollution.CleanBuildings.Add(CurrentSpawn);
    }

    void FlyingSpawn()
    {
        GameObject flyingPrefab = flyingPrefabs[Random.Range(0, flyingPrefabs.Length)];
        Instantiate(flyingPrefab, planet.transform.position, Random.rotation);

    }

    void TimedSpawn()
    {
        Spawn();
        float nextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        Invoke("TimedSpawn", nextSpawn);
    }

    void TimedFlyingSpawn()
    {
        FlyingSpawn();
        float nextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        Invoke("FlyingSpawn", nextSpawn);
    }
}
