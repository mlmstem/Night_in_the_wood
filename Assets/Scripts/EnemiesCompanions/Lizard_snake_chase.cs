using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lizard_snake_chase : MonoBehaviour
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
    public Vector3 walkPoint;
    public Vector3 playerpoint;
    public Vector3 enemypoint;
    bool walkPointSet;
    bool monkey_eat;
    bool freeze;
    public float walkPointRange;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public AudioSource animalSound;

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

    void Update()
    {
        counter = counter + 1;
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        playerInSightRange = distance < sightRange;
        playerInAttackRange = distance < attackRange;
        if ((!playerInSightRange && !playerInAttackRange)) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        if (counter % 30 == 0)
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
        //Debug.Log("chase");
        
        isAttacking = false;
        playerpoint = new Vector3(player.position.x, 0, player.position.z);
        animalSound.Play();
        agent.SetDestination(playerpoint);

    }

    private void AttackPlayer()
    {
        if (Time.timeScale == 0f) {
            return;
        }
        
        //Debug.Log("attack");
        isAttacking = true;
        animalSound.Play();
        //Make sure enemy doesn't move
        enemypoint = new Vector3(transform.position.x, 0, transform.position.z);
        agent.SetDestination(enemypoint);
        transform.LookAt(player);
        animator.SetTrigger("AttackTR");
    }
}

