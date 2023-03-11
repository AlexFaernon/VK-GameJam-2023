using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flyer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed;
    private bool _controlledByPLayer;
    private Vector2 _direction;
    private const int ShootRadius = 5;
    private bool _canShoot = true;

    void Update()
    {
        if (_controlledByPLayer)
        {
            player.transform.position = transform.position;
            transform.Translate(Time.deltaTime * speed * _direction);
        }
        else
        {
            var playerVector = player.transform.position - transform.position;
            if (playerVector.magnitude < ShootRadius)
            {
                playerVector = -playerVector;
            }
            transform.Translate(Time.deltaTime * speed * playerVector.normalized);
        }

        if (!_canShoot) return;
        if (_controlledByPLayer && !Input.GetMouseButtonDown(0) && !Input.GetMouseButton(0)) return;
        
        Shoot();
        _canShoot = false;
        StartCoroutine(ShootCooldown());
    }
    
    public void OnFlyerMove(InputValue context)
    {
        _direction = context.Get<Vector2>();
    }

    public void PlayerControl(bool controlled)
    {
        player.SetActive(!controlled);
        _controlledByPLayer = controlled;
    }

    private void Shoot()
    {
        var bulletObj = Instantiate(bullet, transform.position, new Quaternion());
        Vector2 direction;
        if (_controlledByPLayer)
        {
            direction = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
        }
        else
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        bulletObj.GetComponent<Rigidbody2D>().velocity += direction * 10;
        Destroy(bulletObj, 10);
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(1);

        _canShoot = true;
    }
}