using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mandelbrick : MonoBehaviour
{
    public GameObject extractionArea;
    // Start is called before the first frame update
    void Start()
    {
        extractionArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Destroy(this.gameObject);
            extractionArea.SetActive(true);
        }
    }
}
    