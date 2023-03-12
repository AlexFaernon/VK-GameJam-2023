using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyDumpy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float jumpForce = 10;
    private GameObject _player;
    private bool _canJump;
    private Rigidbody2D _rb;
    private bool _controlledByPLayer;
    private int _health = 10;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (value <= 0)
            {
                Destroy(gameObject);
                GameScore.Score++;
            }
        }
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canJump) {return;}

        if (_controlledByPLayer)
        {
            MoveByPlayer();
        }
        else
        {
            Move();
        }
    }

    public void PlayerControl(bool isControl)
    {
        _controlledByPLayer = isControl;
        if (!isControl)
        {
            Destroy(gameObject);
            GameScore.Score++;
        }
    }

    private void MoveByPlayer()
    {
        if (!_canJump || !Input.GetMouseButtonDown(0) && !Input.GetMouseButton(0)) return;
        
        var direction = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position))
            .normalized;
        
        _rb.velocity = direction * jumpForce;
        _canJump = false;
    }

    private void Move()
    {
        Vector2 direction;
        if (_player.transform.position.x < transform.position.x)
        {
            direction = Quaternion.AngleAxis(30, Vector3.forward) * Vector2.up;
        }
        else
        {
            direction = Quaternion.AngleAxis(-30, Vector3.forward) * Vector2.up;

        }
        
        _rb.velocity = direction * jumpForce;
        _canJump = false;
    }

    private void Shoot()
    {
        var step = 360 / 12;
        var direction = Vector2.up;
        for (var i = 0; i < 12; i++)
        {
            var bulletObj = Instantiate(bullet, transform.position, new Quaternion());
            bulletObj.tag = _controlledByPLayer ? "PlayerBullet" : "MonsterBullet";
            bulletObj.GetComponent<Rigidbody2D>().velocity += direction * 5;
            Destroy(bulletObj, 2);
            direction = Quaternion.AngleAxis(step, Vector3.forward) * direction;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_controlledByPLayer && col.CompareTag("MonsterBullet") ||
            !_controlledByPLayer && col.CompareTag("PlayerBullet"))
        {
            Health--;
            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Ground")) return;
        
        Shoot();
        transform.rotation = new Quaternion();
        if (!_canJump)
        {
            StartCoroutine(JumpCooldown());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;

        StopAllCoroutines();
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(1);

        _canJump = true;
    }
}
