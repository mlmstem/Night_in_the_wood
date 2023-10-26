using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{


    //public Transform playerPos;
    //public Transform offscreenPos;
    //public float speed;
    public GameObject Canvas;

    public GameObject bear;
    public GameObject monkey;
    public GameObject lizard;
    public GameObject snake;
    public GameObject deer;
    public GameObject player;


    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    public GameObject Image5;

    private bool active = false;
    void Start()
    {
        Canvas.SetActive(active);
    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.M))
       {
            Canvas.SetActive(!active);
            active = !active;
        }
       
    
        float distance_bear = Vector3.Distance(bear.transform.position, player.transform.position);
        float distance_monkey = Vector3.Distance(monkey.transform.position, player.transform.position);
        float distance_snake = Vector3.Distance(snake.transform.position, player.transform.position);
        float distance_lizard = Vector3.Distance(lizard.transform.position, player.transform.position);
        float distance_deer = Vector3.Distance(deer.transform.position, player.transform.position);
        if (distance_bear < 5)
        {
            Image1.SetActive(false);
        }
        if (distance_monkey < 5)
        {
            Image2.SetActive(false);
        }
        if (distance_lizard < 5)
        {
            Image3.SetActive(false);
        }
        if (distance_snake < 5)
        {
            Image4.SetActive(false);
        }
        if (distance_deer < 5)
        {
            Image5.SetActive(false);
        }
    }
    
}
