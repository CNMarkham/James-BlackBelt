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
    public bool shooting;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        // destination = agent.destination;
        //debugDistance = GameObject.Find("DistanceDebugText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        //debugDistance.text = Vector3.Distance(transform.position, target.transform.position).ToString();
        destination = target.transform.position;
        agent.destination = destination;
        //Debug.Log(Vector3.Distance(transform.position, target.transform.position));
    
        if (Vector3.Distance(transform.position, target.transform.position) < 10.0f)
        {
            if (shooting == false)
            {
                StartCoroutine(ShootProjectile(1));
            }

        }
    }

    IEnumerator ShootProjectile(float reloadTime)
    {
        shooting = true;
        yield return new WaitForSeconds(reloadTime);
        GameObject clone = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(reloadTime);
        shooting = false;
    }
}
