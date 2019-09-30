using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
    public MapPlayer player;

    void Awake() {
        Cursor.visible = false;
        Transform playerTransform = player.GetComponent<Transform>();
        Vector3 startPosition = InfoHolder.GetMapPosition();
        if (startPosition != Vector3.zero) {
            playerTransform.position = startPosition;
        }
    }

    void Update() {
        player.Controls();
    }
}
