using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=DLAIYSMYy2g

public class SpawnItem : MonoBehaviour
{

    public GameObject item;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public void SpawnDroppedItem()
    {
        Vector3 spawnOffset = player.transform.forward * 3.0f;
        Vector3 spawnPosition = player.transform.position + spawnOffset + Vector3.up * 3.0f;

        Instantiate(item, spawnPosition, Quaternion.identity);
    }
}