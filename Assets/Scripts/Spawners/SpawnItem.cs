using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=DLAIYSMYy2g

public class SpawnItem : MonoBehaviour
{

    public GameObject item;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        Vector3 spawnOffset = player.transform.forward * 3.0f;
        Vector3 spawnPosition = player.transform.position + spawnOffset + Vector3.up * 2.0f;
        Quaternion rotation = Quaternion.identity; // Default rotation

        if (item.name.Contains("Shelter"))
        {
            rotation = Quaternion.Euler(-90f, 0f, 0f); // Rotate by -90 degrees on the x-axis if shelter
        }

        Instantiate(item, spawnPosition, rotation);
    }
}