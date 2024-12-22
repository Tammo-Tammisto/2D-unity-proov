using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject dangerObjectToSpawn;

    public float spawnRate = 1f;
    public float dangerSpawnRate = 3f;

    public float spawnRangeX = 5f;
    public float spawnHeight = 10f;

    public bool isRaining = false;
    private Coroutine rainCoroutine;
    private Coroutine dangerRainCoroutine;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<GameObject> spawnedDangerObjects = new List<GameObject>();

    public void StartRaining()
    {
        isRaining = true;
        if (rainCoroutine == null) // Start the coroutine only if it’s not already running
        {
            rainCoroutine = StartCoroutine(SpawnObjects());
        }

        if (dangerRainCoroutine == null) // Start the danger coroutine only if it’s not already running
        {
            dangerRainCoroutine = StartCoroutine(SpawnDangerObjects());
        }
    }

    IEnumerator SpawnObjects()
    {
        while (isRaining)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRangeX, spawnRangeX),
                spawnHeight,
                0
            );

            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);
            yield return new WaitForSeconds(spawnRate);
        }
        rainCoroutine = null; // Reset the coroutine reference when it stops
    }

    IEnumerator SpawnDangerObjects()
    {
        while (isRaining)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRangeX, spawnRangeX),
                spawnHeight,
                0
            );

            GameObject spawnedDangerObject = Instantiate(dangerObjectToSpawn, spawnPosition, Quaternion.identity);
            spawnedDangerObjects.Add(spawnedDangerObject);
            yield return new WaitForSeconds(dangerSpawnRate);
        }
        dangerRainCoroutine = null; // Reset the coroutine reference when it stops
    }

    public void StopRaining()
    {
        isRaining = false;

        // Destroy all spawned objects
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();

        // Destroy all spawned danger objects
        foreach (GameObject dangerObj in spawnedDangerObjects)
        {
            if (dangerObj != null)
            {
                Destroy(dangerObj);
            }
        }
        spawnedDangerObjects.Clear();
    }
}