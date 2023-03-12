using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 offset;

    private void OnEnable()
    {
        transform.position = player.position + (Vector3)offset;
        transform.rotation = player.rotation;
    }

    private void Update()
    {
        transform.position = player.position + (Vector3)offset;
        transform.rotation = player.rotation;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("MonsterBullet")) return;

        col.tag = "PlayerBullet";
        var rb = col.GetComponent<Rigidbody2D>();
        rb.velocity = -rb.velocity;
    }
}
