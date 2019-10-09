using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayer : MonoBehaviour {
    bool moveable = true;
    Vector2 positionToMoveTo;
    [SerializeField] Position currentPosition;
    public ExitTo exitTo;


    public void Controls() {
        if (moveable) {
            if (Input.GetKeyDown(KeyCode.UpArrow) && currentPosition.up != null) {
                StartCoroutine(MoveToPosition(currentPosition.up.position));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currentPosition.down != null) {
                StartCoroutine(MoveToPosition(currentPosition.down.position));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && currentPosition.right != null) {
                StartCoroutine(MoveToPosition(currentPosition.right.position));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentPosition.left != null) {
                StartCoroutine(MoveToPosition(currentPosition.left.position));
            }
            else if (Input.GetKeyDown(KeyCode.Return) && currentPosition.sceneToLoad != null) {
                exitTo.sceneToLoad = currentPosition.sceneToLoad;
                InfoHolder.SetMapPosition(transform.position);
                StartCoroutine(exitTo.FadeOut());
            }
        }
    }

    public void SetPosition(Position pos) {
        currentPosition = pos;
    }

    IEnumerator MoveToPosition(Vector3 pos) {
        moveable = false;
        while (Vector3.Distance(transform.position, pos) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, pos, 0.5f);
            yield return new WaitForFixedUpdate();
        }
        moveable = true;
    }
}
