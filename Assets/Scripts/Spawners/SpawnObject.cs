using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    private float gameLength = 300.0f; // Spawn for length of game
    private float MinTime = 1.0f;
    private float MaxTime = 300.0f;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        float startTime = Time.time;
        int spawnCount = 0;
        GameObject objectInstance = null;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Object spawns in maximum of 3 times in the 5 minutes
        while (Time.time - startTime < gameLength && spawnCount < 3)
        {

            if (GameObject.FindWithTag("Enemy") == null)
            {
                yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));

                // Manual Spawn location
                // Vector3 spawnPosition = new Vector3(-40, 20, 120);
                // objectInstance = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                // Spawn rain on top of player - ToDo CHANGE FOR ENEMY SPAWN
                objectInstance = Instantiate(objectToSpawn, player.transform.position + new Vector3(0, 18, 0), Quaternion.identity, player.transform);

                spawnCount++;
                StartCoroutine(DestroyAfterDelay(objectInstance, 20.0f));
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }

    }

    IEnumerator DestroyAfterDelay(GameObject objectInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(objectInstance);
    }
}
