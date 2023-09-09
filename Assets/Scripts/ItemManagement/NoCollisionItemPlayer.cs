using UnityEngine;

public class NoCollisionItemPlayer : MonoBehaviour
{
    public GameObject player;
    public string item = "Item";

    private void Start()
    {

        player.layer = LayerMask.NameToLayer("Default");

        // Set up initial collisions
        SetupCollisions();

        // Continuously monitor for new items
        StartCoroutine(MonitorForNewObjects());
    }

    void SetupCollisions()
    {
        // Find all GameObjects with the specified tag
        GameObject[] items = GameObject.FindGameObjectsWithTag(item);

        // Set up a custom collision matrix
        foreach (GameObject itm in items)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), itm.GetComponent<Collider>());
        }
    }

    System.Collections.IEnumerator MonitorForNewObjects()
    {
        while (true)
        {

            yield return new WaitForSeconds(2.0f);
            GameObject[] newItems = GameObject.FindGameObjectsWithTag(item);

            // Check if any new items have been found
            if (newItems.Length > 0)
            {
                // Set up collisions for the new items
                foreach (GameObject newItem in newItems)
                {
                    Physics.IgnoreCollision(player.GetComponent<Collider>(), newItem.GetComponent<Collider>());
                }
            }
        }
    }
}
