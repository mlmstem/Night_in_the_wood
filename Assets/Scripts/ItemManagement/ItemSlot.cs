using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=DLAIYSMYy2g

public class ItemSlot : MonoBehaviour
{

    private PlayerHotbar hotbar;
    public int index;

    private void Start()
    {
        hotbar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHotbar>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            hotbar.isFull[index] = false;
        }
    }

}