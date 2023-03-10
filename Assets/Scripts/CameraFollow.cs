using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = player.position - Vector3.forward * 10;
    }
}
