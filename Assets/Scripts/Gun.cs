using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private bool _canShoot = true;
    private void Update()
    {
        TurnToMouse();

        if (!_canShoot || !Input.GetMouseButtonDown(0) && !Input.GetMouseButton(0)) return;
        
        var bulletObj = Instantiate(bullet, transform.position, new Quaternion());
        bulletObj.tag = "PlayerBullet";
        var direction = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
        bulletObj.GetComponent<Rigidbody2D>().velocity += direction * 20;
        Destroy(bulletObj, 10);
        
        _canShoot = false;
        StartCoroutine(ShootCooldown());
    }

    private void TurnToMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);//немного магии на последок
    }
    
    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(1);

        _canShoot = true;
    }

    private void OnEnable()
    {
        _canShoot = true;
    }
}
