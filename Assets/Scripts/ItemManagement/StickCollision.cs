using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
{
    // Check if the trigger collision is with the player.
    if (other.CompareTag("Enemy"))
    {
        // Assuming the player is tagged as "Player."
        
        // Get the AnimalBehavior script attached to the player.
        Debug.Log("Trigger Enter");
        AnimalBehavior animalBehavior = other.GetComponent<AnimalBehavior>();
        
        if (animalBehavior != null)
        {
            // Call the HandleStickHit method to handle the trigger collision.
            animalBehavior.HandleStickHit();
        }
    }
}

}
