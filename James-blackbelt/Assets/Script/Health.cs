using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void hurtPlayer(float damgae)
    {
        health -= damgae;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

  
}
