using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private int _jumpCount;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _jumpCount < 2)
        {
            _jumpCount++;
            _rb.velocity = new Vector2(_rb.velocity.x, 10);
        }
        
        _rb.velocity = new Vector2(_direction.x * 5, _rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _jumpCount = 0;
        }
    }
}