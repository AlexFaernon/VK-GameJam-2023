using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject barrier;
    [SerializeField] private Image healthBar;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private int _jumpCount;
    private const int MaxHealth = 10;
    private int _health = 10;

    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            healthBar.fillAmount = (float)_health / MaxHealth;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
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

    public void OnPlayerMove(InputValue context)
    {
        _direction = context.Get<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _jumpCount = 0;
            transform.rotation = new Quaternion();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("MonsterBullet"))
        {
            Health--;
            Destroy(col.gameObject);
        }
    }
}