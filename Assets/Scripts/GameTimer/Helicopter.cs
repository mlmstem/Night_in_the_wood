using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{

    public float fallSpeed = 5f;
    public float yPosition = 50f;
    public UnityEngine.AI.NavMeshAgent agent;
    private Vector3 walkPoint;
    void Start()
    {
        gameObject.active = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // Spawn helicopter in 15 second before end of game
        Invoke("wakeup", 290f);
        
        //agent.SetDestination(walkPoint);
    }

    void wakeup()
    {
        gameObject.active = true;
        //agent.SetDestination(walkPoint);
    }
    void update()
    {
        //gameObject.active = true;
        //walkPoint = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
        //agent.SetDestination(walkPoint);
        //transform.position.y = transform.position.y - fallSpeed * Time.deltaTime;
        //transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        yPosition -= fallSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }


}
