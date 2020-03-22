using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
    public MapPlayer player;
    public Animator uitlegAnimator;
    bool canMove = true;

    void Awake() {
        Cursor.visible = false;
        Transform playerTransform = player.GetComponent<Transform>();
        Vector3 startPosition = InfoHolder.GetMapPosition();
        if (startPosition != Vector3.zero) {
            playerTransform.position = startPosition;
        }
    }

    void Start() {
        if (InfoHolder.showUitleg) {
            StartCoroutine(DelayMovement());
            uitlegAnimator.SetBool("show", true);
            InfoHolder.showUitleg = false;
        }
    }

    void Update() {
        if (canMove) {
            player.Controls();
        }
    }

    IEnumerator DelayMovement() {
        canMove = false;
        uitlegAnimator.SetBool("show", false);
        yield return new WaitForSeconds(6f);
        canMove = true;
    }
}
