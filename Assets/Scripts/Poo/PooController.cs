using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PooController : MonoBehaviour
{
    public GameObject ghostObject;
    public Transform chewTargetL;
    public Transform chewTargetR;
    public Transform[] spawnPointsL;
    public Transform[] spawnPointsR;
    public Poo pooIsland;
    public Animator fadeAnim;
    public PooPlayer player;
    public GameObject spaceBar;

    public static int ghostCount = 0;

    bool leftGhost = false;
    bool end = false;
    bool movable = false;

    void Start()
    {
        ghostCount = 0;
        movable = false;
        end = false;
        leftGhost = false;
        StartCoroutine(startGame());
    }

    void Update()
    {
        if (ghostCount == 20 && !end) {
            CancelInvoke("SpawnGhost");
            if (GameObject.FindGameObjectsWithTag("Ghost").Length < 1) {
                end = true;
            }
        }
        if (!end) {
            pooIsland.MoveUpDown();
        }
        else {
            StartCoroutine(endGame());
        }
    }

    void FixedUpdate() {
        if (movable) {
            player.Move();
        }
    }

    IEnumerator startGame() {
        yield return new WaitForSeconds(4.3f);
        InvokeRepeating("SpawnGhost", 1f, 0.7f);
        movable = true;
    }

    IEnumerator endGame() {
        movable = false;
        pooIsland.MoveOutOfSight();
        spaceBar.SetActive(false);
        yield return new WaitForSeconds(3f);
        fadeAnim.SetTrigger("fadeout");
        yield return new WaitForSeconds(1.2f);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterController>().SetPoepWaarde(-30);
        SceneManager.LoadScene(InfoHolder.GetLastScene());
    }

    void SpawnGhost() {
        Vector3 dest;
        if (leftGhost) {
            dest = spawnPointsL[Random.Range(0, spawnPointsL.Length)].position;
            InstantiateGhost(dest, chewTargetL, false);
        }
        else {
            dest = spawnPointsR[Random.Range(0, spawnPointsR.Length)].position;
            InstantiateGhost(dest, chewTargetR, true);
        }
        leftGhost = !leftGhost;
    }

    void InstantiateGhost(Vector3 destination, Transform chewTarget, bool faceLeft)
    {
        Vector3 spawnPoint = Vector3.zero;
        GameObject ghost = Instantiate(ghostObject, spawnPoint, Quaternion.identity);
        ghost.transform.position = destination;
        ghost.GetComponent<Ghost>().target = chewTarget;
        if (faceLeft) {
            ghost.transform.eulerAngles = new Vector3(0,180,0);
        }
        ghostCount++;
    }
}
