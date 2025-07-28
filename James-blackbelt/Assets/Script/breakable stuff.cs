using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablebox : MonoBehaviour, IDamageable
{

    public float hits;
    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hits >= 5)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            hits += 1;
        }
    }
    public void Damage(float damage)
    {
        Destroy(this.gameObject);
    }
}
