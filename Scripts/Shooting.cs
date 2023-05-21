using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform GunPoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    void Update()
    {
        // when there's pause/start/gameover canvas are on shooting is not allowed
        if (GameManager.isCanvasOn == false)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, GunPoint.position, GunPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(GunPoint.up * bulletForce, ForceMode2D.Impulse);
    }
}