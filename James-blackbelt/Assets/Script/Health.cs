using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health;
    public GameObject Hslider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void hurtPlayer(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }

        Hslider.GetComponent<Slider>().value = health;
        
    }

  
}
