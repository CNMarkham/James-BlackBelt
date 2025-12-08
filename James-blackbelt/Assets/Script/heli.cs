using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heli : MonoBehaviour
{
    public float moveSpeed;
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
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            Destroy(this.gameObject, 10f);
        }
    }
}
