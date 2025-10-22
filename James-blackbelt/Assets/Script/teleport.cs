using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunkerEnters : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;
    public Transform target;
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
             player.transform.position = target.position;   
             gun.transform.position = target.position;
        }
    }

}
