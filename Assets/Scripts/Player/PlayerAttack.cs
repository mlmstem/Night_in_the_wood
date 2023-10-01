using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller; // Assuming the player uses CharacterController.

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check for attack input (e.g., using the 'Q' key).
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Trigger the attack animation.
            animator.SetTrigger("Attack");
        }

        // Check if the player is moving forward.
        bool isMovingForward = controller.velocity.magnitude > 0.1f;

        // Set the "isRunning" parameter based on player movement.
        animator.SetBool("isRunning", isMovingForward);
    }
}


