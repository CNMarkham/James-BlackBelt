using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objective : MonoBehaviour
{
    public GameObject textSet1;
    public GameObject textSet1_5;
    public GameObject textSet2;
    public GameObject textSet2_5;
    public GameObject textSet3;
    public GameObject textSet3_5;
    public GameObject textSet4;
    public GameObject textSet4_5;
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
            Destroy(this.gameObject);
            textSet1.SetActive(false);
            textSet1_5.SetActive(false);
            textSet2.SetActive(true);
            textSet1_5.SetActive(true);
        }
    }
}
