using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    public GameObject stickObject;
    private Collider stickCollider;
    public float colliderEnableDuration = 2.0f;
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
        if (stickObject != null && stickObject.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            // Trigger the attack animation and sound
            animator.SetTrigger("Attack");
            attack.Play();

            // Enable the collider when attacking and start the coroutine.
            StartCoroutine(EnableColliderAfterDelay());
        }
    }

    private IEnumerator EnableColliderAfterDelay()
    {
        yield return new WaitForSeconds(0.3f);

        // Enable the collider
        if (stickCollider != null)
        {
            stickCollider.enabled = true;
            StartCoroutine(DisableColliderAfterDelay());
        }
    }

    // Coroutine to disable the collider after the specified duration.
    private IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(colliderEnableDuration);
        stickCollider.enabled = false;
    }
}
