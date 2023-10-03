using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=DLAIYSMYy2g

public class PickupItem : MonoBehaviour
{

    private PlayerHotbar hotbar;
    public GameObject itemButton;
    public bool monkey_eats;
    private float counter = 0;

    void Start()
    {
        hotbar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHotbar>();
    }
    void Update()
    {
        counter = counter + 1;
        if (counter % 10000 == 0)
        {
            //monkey_eats = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < hotbar.slots.Length; i++)
            {
                if (hotbar.isFull[i] == false)
                {
                    Debug.Log("itemAdded");
                    // Item can be added to hotbar
                    hotbar.isFull[i] = true;
                    Instantiate(itemButton, hotbar.slots[i].transform, false);
                    Destroy(transform.parent.gameObject);
                    break;
                }
            }
        }
        if (other.CompareTag("Monkey"))
        {
            monkey_eats = true;
            Debug.Log("monkey_eats");
            Destroy(transform.parent.gameObject);
            

        }
    }

}