using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_brain : Base_Planet
{
    public Transform target;

    private NavMeshAgent agent;
    private Player_Controller pc;

    public LayerMask whatIsGround, whatIsPlayer;

    //States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float chaseTime;

    void Start()
    {
        pc = GetComponent<Player_Controller>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        Initialize();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange)
            chaseTime = Time.time + 3;

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if ((playerInSightRange && !playerInAttackRange) || (chaseTime >= Time.time && !agent.isStopped)) ChasePlayer();
        else
        {
            if (agent.isStopped)
                agent.isStopped = false;
        }
        if (playerInAttackRange && playerInSightRange) Attack();

        if (health <= 0)
        {
            Exploed();
            Destroy(gameObject);
        }
    }

    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 10f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        var distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > attackRange / 2)
            agent.SetDestination(target.position);
        else
            agent.isStopped = true;
    }

    void Attack()
    {
        transform.LookAt(target);
        //Проверка попадания луча
        Ray ray = new Ray(transform.position,target.transform.position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (agent.enabled && Time.time >= timeToFire)
                {
                    timeToFire = Time.time + 1 / effectToSpawn.GetComponent<Projectile>().fireRate;
                    SpawnFVX();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
