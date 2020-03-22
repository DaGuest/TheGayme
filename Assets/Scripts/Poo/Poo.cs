using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poo : MonoBehaviour
{
    float amplitude = 1.125f;
    float verticalSpeed = 0.8f;
    float horizontalSpeed = 5f;
    Vector2 tempPosition;

    void Start() {
        tempPosition = transform.position;
    }

    public void MoveUpDown()
    {
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;
    }

    public void MoveOutOfSight() {
        if (transform.position.x < 17) {
            transform.Translate(Vector2.right * Time.deltaTime * horizontalSpeed);
        }
    }
}
