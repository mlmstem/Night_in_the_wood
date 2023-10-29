using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    private float gameLength = 300.0f; // Spawn for length of game
    private float MinTime = 25.0f; // Rain can't spawn before 20 sec have elapsed
    private float MaxTime = 300.0f;
    public AudioSource rainsound;
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
            if (GameObject.FindWithTag("Rain") == null)

            {

                yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));

                // Spawn rain on top of player
                objectInstance = Instantiate(objectToSpawn, player.transform.position + new Vector3(0, 18, 0), Quaternion.identity, player.transform);

                rainsound.Play();
                StartCoroutine(StopAudioAfterDelay(rainsound, 20f));


                spawnCount++;
                StartCoroutine(DestroyAfterDelay(objectInstance, 20.0f));
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }

    }

    IEnumerator StopAudioAfterDelay(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
    }

    IEnumerator DestroyAfterDelay(GameObject objectInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(objectInstance);
    }
}
