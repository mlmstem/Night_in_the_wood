using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 1.0f; // Adjust the fall speed as needed
    public float terminalVelocity = 2.0f; // Adjust the terminal velocity if necessary
    private Vector3 initialPosition;
    public MeshRenderer PLS1;
    public MeshRenderer PLS2;
    public MeshRenderer PLS3;
    public MeshRenderer PLS4;
    public MeshRenderer PLS5;
    public MeshRenderer PLS6;
    public MeshRenderer PLS7;
    public MeshRenderer PLS8;
    //public float timeLeft = 300.0f;

    private void Start()
    {
        initialPosition = transform.position;
        //gameObject.SetActive(false);
        PLS1.enabled = false;
        PLS2.enabled = false;
        PLS3.enabled = false;
        PLS4.enabled = false;
        PLS5.enabled = false;
        PLS6.enabled = false;
        PLS7.enabled = false;
        PLS8.enabled = false;
    }

    private void Update()
    {
        //timeLeft -= Time.deltaTime;
        //if (timeLeft <= 30)
        //{
            
        //    gameObject.SetActive(true);
            //PLS.enabled = true;

        //}
        
        Vector3 newPosition = transform.position - Vector3.up * fallSpeed * Time.deltaTime;
        transform.position = newPosition;

        if (transform.position.y <= 38)
        {
            // Reset the position to the initial position
            //gameObject.SetActive(true);
            PLS1.enabled = true;
            PLS2.enabled = true;
            PLS3.enabled = true;
            PLS4.enabled = true;
            PLS5.enabled = true;
            PLS6.enabled = true;
            PLS7.enabled = true;
            PLS8.enabled = true;

        }
    }
}