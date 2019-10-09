using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public string sceneToLoad;

    void OnTriggerEnter2D(Collider2D other) {
        other.GetComponent<MapPlayer>().SetPosition(this);
    }
}
