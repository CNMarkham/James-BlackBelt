using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class enemie : MonoBehaviour, IDamageable
{
    public GameObject Findplayer;
    public GameObject bullet;
    public GameObject barrel;
    public GameObject enemy;
    public GameObject insurgent;
    public GameObject point1;
    public GameObject point2;
    public Transform facePlayer;
    public Transform defaultSetup;
    private TMP_Text debugDistance;
    public float health;
    public float bulletSpeed;
    public bool shooting;
    public bool PlayerFound;
    Vector3 destination;
    NavMeshAgent agent;

    void Start()
    {
        defaultSetup = transform;
        agent = GetComponent<NavMeshAgent>();
        Findplayer = GameObject.Find("Player");
        facePlayer = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 flattenedVector = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        transform.forward = flattenedVector;

        if (Vector3.Distance(transform.position, Findplayer.transform.position) < 100.0f && PlayerFound == true)
        {
            if (shooting == false)
            {
                StartCoroutine(ShootProjectile(0.5f));
            }

        }

        if (PlayerFound == true)
        {
            insurgent.transform.LookAt(facePlayer);
        }
        else if(!PlayerFound)
        {
            
            insurgent.transform.rotation = defaultSetup.rotation;
        }



    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerFound = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerFound = false;
        }
    }

    IEnumerator ShootProjectile(float reloadTime)
    {
        shooting = true;
        yield return new WaitForSeconds(reloadTime);
        GameObject clone = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody>().AddForce(insurgent.transform.forward * bulletSpeed, ForceMode.Impulse);
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
