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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(currentSlot);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem(currentSlot);
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

    // Get current item and drop it
    private void DropItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            // Check if the slot has an item
            if (slots[slotIndex].transform.childCount > 0)
            {
                GameObject itemGameObject = slots[slotIndex].transform.GetChild(0).gameObject;
                SpawnItem itemToDrop = itemGameObject.GetComponent<SpawnItem>();

                if (itemToDrop != null)
                {
                    itemToDrop.SpawnDroppedItem();
                    GameObject.Destroy(itemGameObject);
                }
            }
        }
    }

    private void UseItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            if (slots[slotIndex].transform.childCount > 0)
            {
                GameObject itemGameObject = slots[slotIndex].transform.GetChild(0).gameObject;
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

                if (playerObject != null)
                {
                    // Get the player health component
                    HealthManager healthManager = playerObject.GetComponent<HealthManager>();

                    if (healthManager != null)
                    {
                        // Heal player and destroy item
                        healthManager.OnHealButtonClick();
                        Destroy(itemGameObject);
                    }
                }
            }
        }
    }

}