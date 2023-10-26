using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopter : MonoBehaviour
{
    // Start is called before the first frame update
    public float fallSpeed = 0.01f;
    void Start()
    { // Use Start or Awake depending on your goal
        gameObject.active = false;
        Invoke("wakeup", 10f);  // Time in seconds
    }

    void wakeup()
    {
        gameObject.active = true;
    }
    private void Update()
    {
        // Move the helicopter downwards in the Y-axis
        if (this.transform.position.y > 10)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

}
