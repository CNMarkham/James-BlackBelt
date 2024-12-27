using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy_spawn;
    public int spawnPoint;
    public GameObject[] spawnPos = new GameObject[9];
    void Start()
    {
        InvokeRepeating("spawn", 2.0f, 1f);
    }

    void spawn()
    {
  
        spawnPoint = Random.Range(0, 8);
        GameObject clone = Instantiate(enemy_spawn,spawnPos [spawnPoint].transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
