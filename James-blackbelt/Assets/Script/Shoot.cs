using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject barrel;
    public float bulletSpeed;
  //  public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //  Rigidbody clone;
            // GameObject clone = Instantiate(bullet, barrel.transform.position, transform.rotation);
            ///transform.Translate(Vector3.forward * Time.deltaTime);
            //Transform parentOfTheParent = Camera.main.transform ;
            //Quaternion rotationOfTheParentOfTheParent = parentOfTheParent.rotation;
            GameObject clone = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
            //clone.transform.forward = this.transform.forward;
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }   
    }
}
