using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
    public float speed;
    //public float FollowSpeed;
    public float distance;
    public int counter;
    //private float angle;
    //private Vector3 walkpoint;
    // private Vector3 moveDirection;
    private Vector3 last_pos;
    private Vector3 last__player_pos;
    private Animator animator;
    //public float NoticeDistance = 5;
    //public float AttackDistance = 1;
    //public RaycastHit Shot;
    // public float TargetDistance;
    public bool isAttacking = false;
    //public PickupItem script;
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
    bool freeze;
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
        last_pos = player.transform.position;
        last__player_pos= player.transform.position;
        animator = GetComponentInChildren<Animator>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        freeze = false;
        if (last__player_pos == player.transform.position) 
        {
            freeze = true;
        }
        counter = counter + 1;
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        playerInSightRange = distance < sightRange;
        //Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = distance < attackRange;
        // Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if ((!playerInSightRange && !playerInAttackRange) || freeze) Patroling();
        if (playerInSightRange && !playerInAttackRange && !freeze) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !freeze) AttackPlayer();
        if (counter % 75 == 0)
        {
            last_pos = transform.position;
            last__player_pos = player.transform.position;
        }
    }
 
    private void Patroling()
    {
        Debug.Log("patrol");
        //animator.SetTrigger("monkey_walk");
        isAttacking = false;
        if (!walkPointSet || last_pos == transform.position || counter%450==0) SearchWalkPoint();

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

        walkPoint = new Vector3(transform.position.x + randomX, 0, transform.position.z + randomZ);
        walkPointSet = true;
    }

    private void ChasePlayer()
    {
        Debug.Log("chase");
        isAttacking = false;
        playerpoint = new Vector3(player.position.x, 0, player.position.z);
        agent.SetDestination(playerpoint);
   
    }

    private void AttackPlayer()
    {
        Debug.Log("attack");
        isAttacking = true;
        //Make sure enemy doesn't move
        enemypoint = new Vector3(transform.position.x, 0, transform.position.z);
        agent.SetDestination(enemypoint);
        transform.LookAt(player);
        animator.SetTrigger("AttackTR");
    }
}



