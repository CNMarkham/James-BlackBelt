using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bullet : MonoBehaviour
{
    public float Heathdecrease;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<enemie>() != null)
        {
            collision.gameObject.GetComponent<enemie>().hurtPlayer(10);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("breakable"))
        {
            Destroy(this.gameObject);
        }
    }
}
