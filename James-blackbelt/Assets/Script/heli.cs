using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heli : MonoBehaviour
{
    public float moveSpeed;
    public bool heliMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if(heliMove == true)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            Destroy(this.gameObject, 20f);
        }
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (collided.gameObject.GetComponent<PlayerMove>() != null)
        {
            heliMove = true;
        }
    }
}
