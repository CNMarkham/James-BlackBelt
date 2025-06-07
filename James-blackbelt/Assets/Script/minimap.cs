using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class minimap : MonoBehaviour
{
    public Transform player;
  void LateUpdate()
    {
        transform.position = player.position + new Vector3(0,100,0);
    }
}