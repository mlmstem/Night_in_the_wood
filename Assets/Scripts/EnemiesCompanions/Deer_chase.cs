using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer_chase : MonoBehaviour
{
    public float speed;
    public float distance;
    public int counter;
    private Vector3 last_pos;
    private Vector3 last__player_pos;
    private Animator animator;
    public bool isAttacking = false;
    public NavMeshAgent agent;
    public Transform player;
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

    public AudioSource deerSound;

    private void Awake()
    {
        player = GameObject.Find("Hiker").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        last_pos = player.transform.position;
        last__player_pos = player.transform.position;
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
        playerInAttackRange = distance < attackRange;
        if ((!playerInSightRange && !playerInAttackRange) || freeze) Patroling();
        if (playerInSightRange && !playerInAttackRange && !freeze) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !freeze) AttackPlayer();
        if (counter % 10 == 0)
        {
            last_pos = transform.position;
            last__player_pos = player.transform.position;
        }
    }

    private void Patroling()
    {
        isAttacking = false;
        if (!walkPointSet || last_pos == transform.position || counter % 450 == 0) SearchWalkPoint();

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
        if (Time.timeScale == 0f) {
            return;
        }
        
        Debug.Log("attack");
        deerSound.Play();
        isAttacking = true;
        //Make sure enemy doesn't move
        enemypoint = new Vector3(transform.position.x, 0, transform.position.z);
        agent.SetDestination(enemypoint);
        transform.LookAt(player);
        animator.SetTrigger("deer_attack");
    }
}

