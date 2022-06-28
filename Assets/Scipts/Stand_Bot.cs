using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Stand_Bot : Base_Planet
{
    public Transform target;

    private float distance;
    private NavMeshAgent agent;
    private Player_Controller pc;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        pc = GetComponent<Player_Controller>();
        agent = GetComponent<NavMeshAgent>();
        Initialize();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (health <= 0)
        {
            Exploed();
            Destroy(gameObject);
        }
        else
        {
            Brains();
            Attack();
        }
    }

    void Brains()
    {
        if (distance > 10)
            agent.enabled = false;

        if (distance < 10)
        {
            agent.enabled = true;
            agent.SetDestination(target.position);
        }
    }

    void Attack()
    {
        agent.transform.LookAt(target);
        if (agent.enabled && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / effectToSpawn.GetComponent<Projectile>().fireRate;
            SpawnFVX();
        }
    }
}
