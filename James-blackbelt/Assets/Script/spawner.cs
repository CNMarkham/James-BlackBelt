using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy_spawn;
    public int spawnPoint;
    public Vector3[] spawnPos = new Vector3[9];
    void Start()
    {
        InvokeRepeating("spawn", 2.0f, 0.3f);
    }

    void spawn()
    {
  
        spawnPoint = Random.Range(0, 8);
        GameObject clone = Instantiate(enemy_spawn,spawnPos [spawnPoint], Quaternion.identity);
    }

    void Update()
    {
        
    }
}
