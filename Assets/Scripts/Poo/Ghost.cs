using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5;
    public ParticleSystem hitParticles;
    Rigidbody2D rb;
    float force = 4;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 dir = target.position - transform.position; ;
        dir = dir.normalized;
        rb.AddForce((dir + AddNoiseOnAngle(-90, 90)) * force);
        //transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed) + AddNoiseOnAngle(-3, 3);
    }

    Vector2 AddNoiseOnAngle(float min, float max)
    {
        // Find random angle between min & max inclusive
        float xNoise = Random.Range(min, max);
        float yNoise = Random.Range(min, max);

        // Convert Angle to Vector2
        Vector3 noise = new Vector3(
          Mathf.Sin(2 * Mathf.PI * xNoise / 360),
          Mathf.Sin(2 * Mathf.PI * yNoise / 360)
        );
        return noise;
    }
}
