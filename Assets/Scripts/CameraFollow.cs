using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objectToFollow;

    private void Update()
    {
        transform.position = objectToFollow.position - Vector3.forward * 10;
    }
}
