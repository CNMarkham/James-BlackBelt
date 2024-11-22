using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class enemie : MonoBehaviour
{
    private TMP_Text debugDistance;
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    public GameObject bullet;
    public GameObject barrel;
    public float bulletSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        // destination = agent.destination;
        debugDistance = GameObject.Find("DistanceDebugText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        debugDistance.text = Vector3.Distance(destination, target.position).ToString();
        if (Vector3.Distance(destination, target.position) < 10.0f)
        {
            destination = target.position;
            agent.destination = destination;

            GameObject clone = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
