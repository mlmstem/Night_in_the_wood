
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    [SerializeField] float retreatDistance = 5f; // Distance to retreat when attacked.
    private Transform player;
    private Vector3 initialPosition;
    private bool isRetreating = false;
    private Animator animator;
    private int hitCounter = 0; // Counter to track hits with a stick.

    public AudioSource soundeffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Calculate the distance between the player and the animal.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within attack range (e.g., within 5 units).
        if (distanceToPlayer < retreatDistance)
        {
            // Move the animal backward when the player is too close.
            soundeffect.Play();
            Retreat();
        }
        else
        {
            // Return the animal to its initial position if it's not retreating.
            if (!isRetreating)
            {
                ReturnToInitialPosition();
            }
        }
    }

    private void Retreat()
    {
        // Calculate the direction to move away from the player.
        Vector3 retreatDirection = (transform.position - player.position).normalized;

        // Move the animal backward.
        transform.Translate(retreatDirection * Time.deltaTime);

        // Set a flag to indicate that the animal is retreating.
        isRetreating = true;

        // Set the "isRunning" parameter to false when retreating.
        animator.SetBool("isRunning", isRetreating);
    }

    private void ReturnToInitialPosition()
    {
        // Move the animal back to its initial position.
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, Time.deltaTime);

        // Reset the retreat flag.
        isRetreating = false;

        // Set the "isRunning" parameter based on the player's movement.
        bool isPlayerMoving = player.GetComponent<CharacterController>().velocity.magnitude > 0.1f;
        animator.SetBool("isRunning", isRetreating);
    }

    // Function to handle stick hits.
    public void HandleStickHit()
    {
        hitCounter++;

        Debug.Log("hitting the enemy");


        animator.SetBool("Dead", true);
        Debug.Log("Dead");

        StartCoroutine(DestroyAfterAnimation());



    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Assuming "Dead" is the trigger that plays the death animation.
        // Wait for the death animation to finish.
        yield return new WaitForSeconds(2);

        // Disable the snake GameObject after the animation is done.
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}
