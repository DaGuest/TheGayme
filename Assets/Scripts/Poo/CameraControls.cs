using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public void Move(float lvlSpeed) {
        transform.position = transform.position + Vector3.down * lvlSpeed * Time.deltaTime;
    }

    public float GetY() {
        return transform.position.y;
    }
}
