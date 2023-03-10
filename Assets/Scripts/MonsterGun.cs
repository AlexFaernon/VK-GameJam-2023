using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject player;
    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            var bulletObj = Instantiate(bullet, transform.position, new Quaternion());
            var direction = (Vector2)(player.transform.position - transform.position).normalized;
            Debug.Log(direction);
            bulletObj.GetComponent<Rigidbody2D>().velocity += direction * 5;
            Destroy(bulletObj, 10);
            yield return new WaitForSeconds(1);
        }
    }
}
