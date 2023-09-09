using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=DLAIYSMYy2g


public class PlayerHotbar : MonoBehaviour
{
    [SerializeField] public bool[] isFull;
    [SerializeField] public GameObject[] slots;
    private int currentSlot = 0;
    private Vector3 originalScale;

    private void Start()
    {
        currentSlot = 0;
        originalScale = slots[currentSlot].transform.localScale;
        UpdateHotbarUI();
    }

    private void Update()
    {

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0f)
        {
            currentSlot += (int)Mathf.Sign(scrollInput);
            currentSlot = Mathf.Clamp(currentSlot, 0, slots.Length - 1);
            UpdateHotbarUI();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DropItem();
        }
    }

    private void UpdateHotbarUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.localScale = originalScale;
        }

        slots[currentSlot].transform.localScale *= 1.2f;
    }

    public void DropItem()
    {
        // Check if the current slot has a valid item to drop
        if (currentSlot >= 0 && currentSlot < transform.childCount)
        {
            Transform item = transform.GetChild(currentSlot);
            item.GetComponent<SpawnItem>().SpawnDroppedItem();
            GameObject.Destroy(item.gameObject);
        }
    }
}