using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{

    public float fallSpeed = 0.01f;
    void Start()
    {
        gameObject.active = false;
        // Spawn helicopter in 15 second before end of game
        Invoke("wakeup", 10f);
    }

    void wakeup()
    {
        gameObject.active = true;
    }
    private void Update()
    {
        // Move the helicopter downwards in the Y-axis
        if (this.transform.position.y > 5)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

}
