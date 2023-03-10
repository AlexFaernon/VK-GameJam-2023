using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject barrier;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private int _jumpCount;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            barrier.SetActive(true);
        }
        else
        {
            barrier.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < 2)
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
            transform.rotation = new Quaternion(); 
        }
    }
}