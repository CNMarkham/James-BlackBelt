using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Gun2;
    public GameObject bullet;
    public GameObject barrel;
    public GameObject barrel2;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clone = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

            clone.GetComponent<Rigidbody>().AddForce(barrel.transform.forward * bulletSpeed, ForceMode.Impulse);
        }

        if (Input.GetMouseButton(0))
        {
            GameObject clone = Instantiate(bullet, barrel2.transform.position, Quaternion.identity);

            clone.GetComponent<Rigidbody>().AddForce(barrel2.transform.forward * bulletSpeed, ForceMode.Impulse);
            Debug.Log("Child GameObject Active: " + Gun2.activeSelf);
        }


    }
}
