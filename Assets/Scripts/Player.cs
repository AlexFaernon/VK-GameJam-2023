using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject barrier;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject gameEnd;
    private SpriteRenderer _sprite;
    private Animator _anim;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private int _jumpCount;
    private const int MaxHealth = 10;
    private int _health = 10;
    private static readonly int Speed = Animator.StringToHash("Speed");

    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            healthBar.fillAmount = (float)_health / MaxHealth;
            if (value <= 0)
            {
                _anim.Play("Die"); // fix
                gameEnd.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_direction == Vector2.zero)
        {
            _anim.Play("Inaction");
        }
        _sprite.flipX = _direction == Vector2.left;
        
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

    private void OnDisable()
    {
        barrier.SetActive(false);
    }

    public void OnPlayerMove(InputValue context)
    {
        _direction = context.Get<Vector2>();
        _anim.Play("Run");
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