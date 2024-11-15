using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemie : MonoBehaviour
{

    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
       // destination = agent.destination;
    }

    void Update()
    {
        if (Vector3.Distance(destination, target.position) < 10.0f)
        {
            destination = target.position;
            agent.destination = destination;

        }
    }
}
