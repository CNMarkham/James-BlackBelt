using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airmine : MonoBehaviour
{
    public float Damage;
    public float radius = 5.0F;
    public float power = 10.0F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collided)
    {
        if (collided.gameObject.GetComponent<PlayerMove>() != null)
        {
            collided.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, this.gameObject.transform.position, radius, 10,ForceMode.Impulse);
            Destroy(this.gameObject,0.5f);
            collided.gameObject.GetComponent<Health>().hurtPlayer(Damage);
        }
    }
}
