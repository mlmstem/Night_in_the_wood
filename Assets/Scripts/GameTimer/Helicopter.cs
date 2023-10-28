using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 1.0f; // Adjust the fall speed as needed
    public float terminalVelocity = 2.0f; // Adjust the terminal velocity if necessary
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new position by moving the object down
        Vector3 newPosition = transform.position - Vector3.up * fallSpeed * Time.deltaTime;

        // Limit the maximum fall speed (terminal velocity)
        if (fallSpeed > terminalVelocity)
        {
            fallSpeed = terminalVelocity;
        }

        // Update the object's position
        transform.position = newPosition;

        // Check if the object has reached the floor (you can customize this condition)
        if (transform.position.y <= 0)
        {
            // Reset the position to the initial position
            transform.position = initialPosition;
            fallSpeed = 0;
        }
    }
}