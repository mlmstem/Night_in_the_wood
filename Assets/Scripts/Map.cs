using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{


    //public Transform playerPos;
    //public Transform offscreenPos;
    //public float speed;
    public GameObject Canvas;
    /*void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, offscreenPos.position, speed * Time.deltaTime);

        }
    }
    */
    private bool active = false;
    void Update()
    {
        Canvas.SetActive(active);
        if (Input.GetKey(KeyCode.M))
        {
            active=true;
        }
        else
        {
            active = false; ;

        }
    }
    
}
