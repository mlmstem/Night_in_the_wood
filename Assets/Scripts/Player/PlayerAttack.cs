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
        // Check for attack input (e.g., left mouse button).
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button.
        {
            // Trigger the attack animation.
            animator.SetTrigger("Attack");
        }
    }
}


