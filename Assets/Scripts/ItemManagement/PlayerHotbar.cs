using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=DLAIYSMYy2g

public class PlayerHotbar : MonoBehaviour
{
    [SerializeField] public bool[] isFull;
    [SerializeField] public GameObject[] slots;
    [SerializeField] private TextMeshProUGUI popupTextCraft;
    [SerializeField] private TextMeshProUGUI popupTextStick;
    [SerializeField] private TextMeshProUGUI popupTextShelter;
    private int currentSlot = 0;
    private Vector3 originalScale;
    [SerializeField] public GameObject craftedItem;
    private bool hasSufficientItems = false;

    [SerializeField] private GameObject log01a; // Reference to the log_01_a GameObject.

    public AudioSource healSound;

    public AudioSource throwSound;

    private void Start()
    {
        currentSlot = 0;
        originalScale = slots[currentSlot].transform.localScale;
        UpdateHotbarUI();
    }

    private void Update()
    {

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Select item slots using numbers on keyboard
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                currentSlot = i;
                UpdateHotbarUI();
            }
        }

        // Select item slots with mouse scroll
        if (scrollInput != 0f)
        {
            currentSlot += (int)Mathf.Sign(scrollInput);
            currentSlot = Mathf.Clamp(currentSlot, 0, slots.Length - 1);
            UpdateHotbarUI();
        }

        if (slots[currentSlot].transform.childCount > 0)
        {
            GameObject itemGameObject = slots[currentSlot].transform.GetChild(0).gameObject;
            if (itemGameObject.name.Contains("Stick"))
            {
                log01a.SetActive(true);
                popupTextStick.gameObject.SetActive(true);

            }
            else
            {
                log01a.SetActive(false);
                popupTextStick.gameObject.SetActive(false);
            }

            if (itemGameObject.name.Contains("Shelter"))
            {
                popupTextShelter.gameObject.SetActive(true);

            }
            else
            {
                popupTextShelter.gameObject.SetActive(false);
            }
        }
        else
        {
            log01a.SetActive(false);
        }



        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(currentSlot);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            throwSound.Play();
            UseItem(currentSlot);
        }

        CheckCrafting();

        if (Input.GetKeyDown(KeyCode.C))
        {
            CraftShelter();
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

                // Get the player health component
                HealthManager healthManager = playerObject.GetComponent<HealthManager>();

                // If nightshade berry, start poison like effect
                if (itemGameObject.name.Contains("Nightshade"))
                {
                    StartCoroutine(RepeatDamageOverTime(5, 2.5f, healthManager));
                    Destroy(itemGameObject);
                }
                else if (!(itemGameObject.name.Contains("Rock")
                || itemGameObject.name.Contains("Stick")
                || itemGameObject.name.Contains("Shelter")))
                {
                    // Heal player if not crafting item rock or stick
                    healthManager.OnHealButtonClick();
                    healSound.Play();
                    // Destroy Item
                    Destroy(itemGameObject);
                }

            }
        }
    }

    private IEnumerator RepeatDamageOverTime(int seconds, float damage, HealthManager healthManager)
    {
        for (int i = 0; i < seconds; i++)
        {
            healthManager.TakeDamage(damage);
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void CheckCrafting()
    {
        int requiredSticks = 2;
        int requiredRocks = 1;
        int sticksDestroyed = 0;
        int rocksDestroyed = 0;

        // Collect references to items to be destroyed
        List<GameObject> itemsToDestroy = new List<GameObject>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                GameObject itemGameObject = slots[i].transform.GetChild(0).gameObject;

                if (itemGameObject.name.Contains("Stick") && sticksDestroyed < requiredSticks)
                {
                    itemsToDestroy.Add(itemGameObject);
                    sticksDestroyed++;
                }
                else if (itemGameObject.name.Contains("Rock") && rocksDestroyed < requiredRocks)
                {
                    itemsToDestroy.Add(itemGameObject);
                    rocksDestroyed++;
                }
            }
        }

        // Check if enough items are found for crafting and set the flag
        hasSufficientItems = (sticksDestroyed == requiredSticks && rocksDestroyed == requiredRocks);

        // Let player know they can craft
        if (hasSufficientItems)
        {
            popupTextCraft.gameObject.SetActive(true);
            popupTextStick.gameObject.SetActive(false);
        }
        else
        {
            popupTextCraft.gameObject.SetActive(false);
        }
    }

    private void CraftShelter()
    {
        if (hasSufficientItems)
        {
            int requiredSticks = 2;
            int requiredRocks = 1;
            int sticksDestroyed = 0;
            int rocksDestroyed = 0;

            // Collect references to items to be destroyed
            List<GameObject> itemsToDestroy = new List<GameObject>();

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].transform.childCount > 0)
                {
                    GameObject itemGameObject = slots[i].transform.GetChild(0).gameObject;

                    if (itemGameObject.name.Contains("Stick") && sticksDestroyed < requiredSticks)
                    {
                        itemsToDestroy.Add(itemGameObject);
                        sticksDestroyed++;
                    }
                    else if (itemGameObject.name.Contains("Rock") && rocksDestroyed < requiredRocks)
                    {
                        itemsToDestroy.Add(itemGameObject);
                        rocksDestroyed++;
                    }
                }
            }

            // Check if enough items are found for crafting and reset the flag
            if (sticksDestroyed == requiredSticks && rocksDestroyed == requiredRocks)
            {
                // Destroy the collected items
                foreach (var itemToDestroy in itemsToDestroy)
                {
                    Destroy(itemToDestroy);
                }

                // Craft the item
                PlayerHotbar hotbar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHotbar>();
                hotbar.isFull[currentSlot] = true;
                Instantiate(craftedItem, hotbar.slots[currentSlot].transform, false);
                healSound.Play();

                // Reset the flag as crafting is complete
                hasSufficientItems = false;
            }
        }
    }









}
