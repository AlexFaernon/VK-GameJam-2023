using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private void Update()
    {
        TurnToMouse();

        if (!Input.GetMouseButtonDown(0)) return;
        
        var bulletObj = Instantiate(bullet, transform.position, new Quaternion());
        var direction = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
        Debug.Log(direction);
        bulletObj.GetComponent<Rigidbody2D>().velocity += direction * 5;
        Destroy(bulletObj, 10);
    }

    private void TurnToMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);//немного магии на последок
    }
}
