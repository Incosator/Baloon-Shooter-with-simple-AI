using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot_Brains : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {

    }

}
