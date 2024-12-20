using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnRate = 1f;
    public float spawnRangeX = 5f;
    public float spawnHeight = 10f;
    private bool isRaining = false;
    private Coroutine rainCoroutine; // To keep track of the coroutine

    public void StartRaining()
    {
        isRaining = true;
        if (rainCoroutine == null) // Start the coroutine only if it’s not already running
        {
            rainCoroutine = StartCoroutine(SpawnObjects());
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

            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        rainCoroutine = null; // Reset the coroutine reference when it stops
    }

    public void StopRaining()
    {
        isRaining = false;
    }
}
