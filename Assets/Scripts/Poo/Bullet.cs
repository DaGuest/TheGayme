using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        effect.transform.parent = Camera.main.transform;
        if (col.transform.tag == "Ghost")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
}
