using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class minimap : MonoBehaviour
{
    public Transform player;
  void LateUpdate()
    {
        Vector3 newPostiton = player.position;
        newPostiton.y = transform.position.y;
        transform.position = newPostiton;
    }
}