using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monkey_chase : MonoBehaviour
{
    public float speed;
    public float distance;
    public int counter;
    private Vector3 last_pos;
    private Animator animator;
    public bool isAttacking = false;
    public bool monkey_eats;
    public NavMeshAgent agent;
    public Transform player;
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

    public AudioSource monkeySound;

    private void Awake()
    {
        player = GameObject.Find("Hiker").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        last_pos = player.transform.position;
        animator = GetComponentInChildren<Animator>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter = counter + 1;
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        playerInSightRange = distance < sightRange;
        playerInAttackRange = distance < attackRange;

        if (monkey_eats)
        {
            // If monkey is eating, disable attack and chase behaviors.
            Patroling();
        }
        else
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            else if (playerInAttackRange)
            {
                AttackPlayer();
            }
        }

        if (counter % 30 == 0)
        {
            last_pos = transform.position;
        }
    }

    private void Patroling()
    {
        animator.SetTrigger("monkey_walk");
        isAttacking = false;
        if (!walkPointSet || last_pos == transform.position) SearchWalkPoint();

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
        monkeySound.Play();
        isAttacking = false;
        playerpoint = new Vector3(player.position.x, 0, player.position.z);
        agent.SetDestination(playerpoint);
        animator.SetTrigger("monkey_walk");
    }

    private void AttackPlayer()
    {
        isAttacking = true;
        //Make sure enemy doesn't move
        enemypoint = new Vector3(transform.position.x, 0, transform.position.z);
        agent.SetDestination(enemypoint);
        transform.LookAt(player);
        animator.SetTrigger("monkey_attack");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            monkey_eats = true;
            Debug.Log("monkey_eats");
            Destroy(other.gameObject);
        }
    }
}



