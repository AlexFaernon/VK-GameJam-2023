using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blevun : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    private bool _controlledByPLayer;
    private Vector2 _direction;
    private bool _canShoot = true;
    private int _health = 6;

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
                ControlEnemy.Charge++;

            }
        }
    }

    void Update()
    {
        if (!_canShoot) return;
        if (_controlledByPLayer && !Input.GetMouseButtonDown(0) && !Input.GetMouseButton(0)) return;
        Shoot();
        _canShoot = false;
        StartCoroutine(ShootCooldown());
    }
    
    public void PlayerControl(bool controlled)
    {
        _controlledByPLayer = controlled;
        if (!controlled)
        {
            Destroy(gameObject);
            GameScore.Score++;
        }
    }
    private void Shoot()
    {
        var bulletObj = Instantiate(bullet, transform.position, new Quaternion());
        bulletObj.tag = _controlledByPLayer ? "PlayerBullet" : "MonsterBullet";
        Vector2 direction;
        if (_controlledByPLayer)
        {
            direction = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
        }
        else
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        bulletObj.GetComponent<Rigidbody2D>().velocity += direction * 7;
        Destroy(bulletObj, 5);
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(0.7f);

        _canShoot = true;
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
}