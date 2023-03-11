using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("MonsterBullet")) return;

        var rb = col.GetComponent<Rigidbody2D>();
        rb.velocity = -rb.velocity;
    }
}
