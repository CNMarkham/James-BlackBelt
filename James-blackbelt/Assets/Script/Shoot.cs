using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Hit;

    public GameObject Gun;
    public GameObject Gun2;
    public GameObject bullet;
    public GameObject barrel;
    public GameObject barrel2;
    public GameObject CameraRotation;
    public float range = 100;
    public float bulletSpeed;

    float previousShot = 0;
    float firerate = 0.1f;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if mouse button down AND if the name of the gameObject this script is attached to is called GUN or GUN2
        if (Input.GetMouseButtonDown(0) && Gun.activeInHierarchy)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range))
            {

            }

           /* GameObject clone = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

            clone.GetComponent<Rigidbody>().AddForce(barrel.transform.forward * bulletSpeed, ForceMode.Impulse);*/
        }
        Debug.DrawRay(transform.position, transform.forward, Color.red, range);
        if (Input.GetMouseButton(0) && Gun2.activeInHierarchy)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Time.time - previousShot > firerate)
            {
                LayerMask mask = LayerMask.GetMask("enemy");
                if (Physics.Raycast(ray, out hit, range, mask))
                {

                    hit.collider.GetComponent<enemie>().hurtPlayer(15);
                    GameObject partical = Instantiate(Hit, hit.point, transform.rotation);
                    Destroy(partical,1);
                    Debug.Log("dmg");
                }
                previousShot = Time.time;
            }
        }
    }
}
