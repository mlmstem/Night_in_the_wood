using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=zssU0MZcIx8
// https://www.youtube.com/watch?v=tveRasxUabo

public class deer_follow : MonoBehaviour
{
    public GameObject Player;
    public float TargetDistance;
    public float AllowedDistance = 2;
    public GameObject Companion;
    public float FollowSpeed;
    public RaycastHit Shot;
    private Animator animator;
    private Vector3 moveDirection;

    void Start()
    {
        //animator = GetComponent<Animator>();
        moveDirection = (Player.transform.position - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * FollowSpeed * Time.deltaTime);
        transform.LookAt(Player.transform);
        TargetDistance = Vector3.Distance(this.transform.position, Player.transform.position);
        if (TargetDistance >= AllowedDistance)
        {
            FollowSpeed = 2f;
                //animator.SetTrigger("FlyTrigger");
                //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, FollowSpeed);
                
                
        }
        else
        {
            FollowSpeed = 0;
                //animator.SetTrigger("StationaryTrigger");
        }
      
    }
}
