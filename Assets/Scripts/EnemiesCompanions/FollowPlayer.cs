using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=zssU0MZcIx8
// https://www.youtube.com/watch?v=tveRasxUabo

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public float TargetDistance;
    public float AllowedDistance = 5;
    public GameObject Companion;
    public float FollowSpeed;
    public RaycastHit Shot;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(Player.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot) && animator != null)
        {
            TargetDistance = Shot.distance;
            if (TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 0.15f;
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, FollowSpeed);
            }
            else
            {
                FollowSpeed = 0;
            }
        }
    }
}
