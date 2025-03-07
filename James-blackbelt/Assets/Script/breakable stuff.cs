using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablebox : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(this.gameObject);
        }
    }
    public void Damage(float damage)
    {
        Destroy(this.gameObject);
    }
}
