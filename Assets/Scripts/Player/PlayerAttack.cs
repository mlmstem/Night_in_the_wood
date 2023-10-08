using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller; // Assuming the player uses CharacterController.
    public GameObject stickObject; // Reference to the stick object.
    private Collider stickCollider; // Reference to the stick's collider.
    public float colliderEnableDuration = 0.5f; // Adjust the duration as needed.

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        
        // Assuming you've assigned the stick object in the Unity Inspector.
        // If not, make sure to assign it there.
        if (stickObject != null)
        {
            stickCollider = stickObject.GetComponent<BoxCollider>();
            
            // Disable the collider initially.
            if (stickCollider != null)
            {
                stickCollider.enabled = false;
            }
        }
    }

    private void Update()
    {
        // Check for attack input (e.g., left mouse button).
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button.
        {
            // Trigger the attack animation.
            animator.SetTrigger("Attack");

            // Enable the collider when attacking and start the coroutine.
            if (stickCollider != null)
            {
                stickCollider.enabled = true;
                StartCoroutine(DisableColliderAfterDelay());
            }
        }
    }

    // Coroutine to disable the collider after the specified duration.
    private IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(colliderEnableDuration);

        if (stickCollider != null)
        {
            stickCollider.enabled = false;
        }
    }
}
