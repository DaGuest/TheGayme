using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    AudioSource shootSound;

    public float bulletForce = 20f;

    private void Start() {
        shootSound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 
        shootSound.Play();
        rb.AddForce(-firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
