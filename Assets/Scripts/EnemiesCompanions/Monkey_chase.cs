using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monkey_chase : MonoBehaviour
{
    public float speed;
    //public float FollowSpeed;
    public float distance;
    public int counter;
    //private float angle;
    //private Vector3 walkpoint;
   // private Vector3 moveDirection;
    private Vector3 last_pos;
    private Animator animator;
    //public float NoticeDistance = 5;
    //public float AttackDistance = 1;
    //public RaycastHit Shot;
   // public float TargetDistance;
    public bool isAttacking = false;
    public PickupItem script1;
    public PickupItem script2;
    public PickupItem script3;
    public PickupItem script4;
    // new version
    public NavMeshAgent agent;
    public Transform player;
    //public LayerMask whatIsGround, whatIsPlayer;
    //Patroling
    public Vector3 walkPoint;
    public Vector3 playerpoint;
    public Vector3 enemypoint;
    bool walkPointSet;
    bool monkey_eat;
    public float walkPointRange;
    //Attacking
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Hiker").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        /*float randomz = Random.Range(-500, 500);
        float randomx = Random.Range(-500, 500);
        walkpoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);*/
        last_pos = player.transform.position;
        animator = GetComponentInChildren<Animator>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        counter=counter+1;
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        playerInSightRange = distance < sightRange;
        //Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = distance < attackRange;
        // Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if ((!playerInSightRange && !playerInAttackRange) || script1.monkey_eats || script2.monkey_eats || script3.monkey_eats || script4.monkey_eats) Patroling();
        if (playerInSightRange && !playerInAttackRange && !script1.monkey_eats && !script2.monkey_eats && !script3.monkey_eats && !script4.monkey_eats) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !script1.monkey_eats && !script2.monkey_eats && !script3.monkey_eats && !script4.monkey_eats) AttackPlayer();
        if (counter% 75 == 0)
        {
            last_pos = transform.position;
        }
    }
   
    private void Patroling()
    {
        //Debug.Log("patrol");
        animator.SetTrigger("monkey_walk");
        isAttacking = false;
        if (!walkPointSet || last_pos== transform.position) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, 0,transform.position.z + randomZ);
        walkPointSet = true;
    }

    private void ChasePlayer()
    {
        //Debug.Log("chase");
        isAttacking = false;
        playerpoint = new Vector3(player.position.x, 0, player.position.z);
        agent.SetDestination(playerpoint);
        animator.SetTrigger("monkey_walk");
    }

    private void AttackPlayer()
    {
        //Debug.Log("attack");
        isAttacking = true;
        //Make sure enemy doesn't move
        enemypoint = new Vector3(transform.position.x, 0, transform.position.z);
        agent.SetDestination(enemypoint);
        transform.LookAt(player);
        animator.SetTrigger("monkey_attack");
    }   
}



