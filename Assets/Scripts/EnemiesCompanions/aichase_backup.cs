using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase_backup : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float FollowSpeed;
    public float distance;
    public int counter;
    private float angle;
    private Vector3 walkpoint;
    private Vector3 moveDirection;
    private Vector3 last_pos;
    private Animator animator;
    public float NoticeDistance = 5;
    public float AttackDistance = 1;
    public RaycastHit Shot;
    //private Animator animator;
    public float TargetDistance;
    public bool isAttacking = false;
    // Start is called before the first frame update


    void Start()
    {
        float randomz = Random.Range(-500, 500);
        float randomx = Random.Range(-500, 500);
        walkpoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);
        last_pos = player.transform.position;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        counter++;

        if (counter % 1000 == 0)
        {
            //private bool isAttacking = false;
            float randomz = Random.Range(-500, 500);
            float randomx = Random.Range(-500, 500);
            walkpoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);
        }
        if (counter % 500 == 0)
        {
            //animator.SetTrigger("WalkTR");
            //animator.SetBool("IsAttacking", false);
        }

        /*
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot) && animator != null)
        {
            TargetDistance = Shot.distance;
            if (last_pos == player.transform.position || TargetDistance >= NoticeDistance)
            {
                FollowSpeed = 0.02f;
                //animator.SetTrigger("FlyTrigger");
                transform.LookAt(walkpoint);
                transform.position = Vector3.MoveTowards(transform.position, walkpoint, FollowSpeed);
            }
            else if (last_pos != player.transform.position)
            {
                FollowSpeed = 0.02f;
                //animator.SetTrigger("FlyTrigger");
                transform.LookAt(player.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, FollowSpeed);
            }
            if (TargetDistance <= AttackDistance)
            {
                animator.SetTrigger("AttackTR");
            }
        }
        last_pos = player.transform.position;
   }

}
        */
        // Move the character in the current direction
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Cast a ray forward to detect collisions
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;

        /*if (Physics.Raycast(ray, out hit, 0.001f)) // You can adjust the ray length as needed
        {
            // If a collision is detected, change the movement direction
            moveDirection = Vector3.Reflect(moveDirection, hit.normal);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        */
        if (last_pos == player.transform.position || distance >= 10)
        {
            isAttacking = false;
            moveDirection = (walkpoint - this.transform.position).normalized;
            //transform.Translate(moveDirection * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            //transform.LookAt(moveDirection);
            //transform.position = Vector3.MoveTowards(transform.position, walkpoint, speed);
        }
        else if (distance < 10 && distance > 0 && last_pos != player.transform.position)
        {
            isAttacking = false;
            moveDirection = (player.transform.position - this.transform.position).normalized;
            //transform.Translate(moveDirection * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            //transform.LookAt(player.transform.position);
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);

        }
        if (distance > 0 && distance <= 5 && last_pos != player.transform.position)
        {
            isAttacking = true;
            moveDirection = (player.transform.position - this.transform.position).normalized;
            //transform.Translate(moveDirection * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            animator.SetTrigger("AttackTR");
            //animator.SetBool("IsAttacking", true);
        }
        if (counter % 75 == 0)
        {
            last_pos = player.transform.position;
        }

    }
}



