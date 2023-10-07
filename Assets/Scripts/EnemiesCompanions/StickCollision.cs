using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {


        // Check if the collision is with the player.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Assuming the player is tagged as "Player."
            
            // Get the AnimalBehavior script attached to the player.

            Debug.Log("Collide");
            AnimalBehavior animalBehavior = collision.gameObject.GetComponent<AnimalBehavior>();
            
            if (animalBehavior != null)
            {
                // Call the HandleStickHit method to handle the collision.
                animalBehavior.HandleStickHit();
            }
        }
    }
}
