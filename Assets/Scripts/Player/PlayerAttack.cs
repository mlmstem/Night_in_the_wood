using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    public GameObject stickObject;
    private Collider stickCollider;
    public float colliderEnableDuration = 0.5f;
    public AudioSource attack;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

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
        // Check for attack input and stick is selected
        if (stickObject != null && stickObject.activeInHierarchy && Input.GetKeyDown(KeyCode.E)) // 0 corresponds to the left mouse button.
        {
            // Trigger the attack animation.
            animator.SetTrigger("Attack");
            attack.Play();

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
