using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class enemie : MonoBehaviour, IDamageable
{
    private TMP_Text debugDistance;
    public GameObject Findplayer;
    Vector3 destination;
    NavMeshAgent agent;
    public GameObject bullet;
    public GameObject barrel;
    public GameObject enemy;
    public float health;
    public float bulletSpeed;
    public bool shooting;

    public Transform facePlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Findplayer = GameObject.Find("Player");
        facePlayer = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 flattenedVector = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        transform.forward = flattenedVector;

        if (Vector3.Distance(transform.position, Findplayer.transform.position) < 10.0f)
        {
            if (shooting == false)
            {
                StartCoroutine(ShootProjectile(1));
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.LookAt(facePlayer);
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
