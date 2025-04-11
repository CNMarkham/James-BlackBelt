using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{

    [SerializeField] private Transform player;

    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}
