using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class enemie : MonoBehaviour
{
    private TMP_Text debugDistance;
    public GameObject target;
    Vector3 destination;
    NavMeshAgent agent;
    public GameObject bullet;
    public GameObject barrel;
    public float bulletSpeed;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        // destination = agent.destination;
        debugDistance = GameObject.Find("DistanceDebugText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        //debugDistance.text = Vector3.Distance(transform.position, target.transform.position).ToString();

        Debug.Log(Vector3.Distance(transform.position, target.transform.position));

        if (Vector3.Distance(transform.position, target.transform.position) < 10.0f)
        {
           // Debug.Log(Vector3.Distance(destination, target.transform.position));
            destination = target.transform.position;
            agent.destination = destination;
            StartCoroutine(ShootProjectile(5));

        }
    }

    IEnumerator ShootProjectile(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        GameObject clone = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(reloadTime);
    }
}
