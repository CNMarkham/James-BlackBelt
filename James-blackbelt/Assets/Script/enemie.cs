using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class enemie : MonoBehaviour, IDamageable
{
    private TMP_Text debugDistance;
    public GameObject Findplayer;
    Vector3 destination;
    NavMeshAgent agent;
    public GameObject bullet;
    public GameObject barrel;
    public float health;
    public float bulletSpeed;
    public bool shooting;

    public Transform facePlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Findplayer = GameObject.Find("Player");
        facePlayer = GameObject.Find("Player").transform;
        // destination = agent.destination;
        //debugDistance = GameObject.Find("DistanceDebugText").GetComponent<TMP_Text>();
    }

    void Update()
    {
      
        transform.LookAt(facePlayer);
        Vector3 flattenedVector = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        transform.forward = flattenedVector;
        //debugDistance.text = Vector3.Distance(transform.position, target.transform.position).ToString();
        destination = Findplayer.transform.position;
        agent.destination = destination;
        //Debug.Log(Vector3.Distance(transform.position, target.transform.position));

        if (Vector3.Distance(transform.position, Findplayer.transform.position) < 10.0f)
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

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
