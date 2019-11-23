using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarController : MonoBehaviour {
    Vector3 poepZoom;
    public Animator battleAnimator;
    public Player player;
    public Text spacebarText;
    private MasterController masterController;

    void Awake() {
        Transform playerTransform = player.GetComponent<Transform>();
        Vector3 startPosition = InfoHolder.GetBarPosition();
        if (!InfoHolder.GetLastScene().Equals("Map") && startPosition != Vector3.zero ) {
            playerTransform.position = startPosition;
        }
        masterController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterController>();
    }
	
    void Start() {
        poepZoom = GameObject.FindGameObjectWithTag("poepzoom").transform.position;
        if (!InfoHolder.playerInfoLoaded) {
            InfoHolder.SetPlayerInfo(player.GetComponent<CharInfo>());
        }
        SubscribeToBehaviours();
    }

    void FixedUpdate() {
        player.Move();
    }

    void SubscribeToBehaviours() {
        player.onBattle += StartBattle;
        player.onBattleReady += BattleReady;
        player.onPoepen += StartPoepen;
    }

    void BattleReady(bool value, string text) {
        if (value) {
            SetSpaceBarText(text, Color.black, 21);
        }
        else {
            SetSpaceBarText("SPACEBAR", Color.grey, 19);
        }
    }

    void SetSpaceBarText(string text, Color color, int size) {
        spacebarText.text = text;
        spacebarText.color = color;
        spacebarText.fontSize = size;
    }

    void StartBattle() {
        InfoHolder.SetBarPosition(player.GetComponent<Transform>().position);
        StartCoroutine(IStartBattle());
    }

    IEnumerator IStartBattle() {
        battleAnimator.SetTrigger("battle");
        yield return new WaitForSeconds(3.5f);
        InfoHolder.SetLastScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Versier");
    }

    void StartPoepen() {
        InfoHolder.SetBarPosition(new Vector3(19.23f, 4.72f, 0));
        StartCoroutine(IStartPoepen());
    }

    IEnumerator IStartPoepen() {
        Camera.main.GetComponent<CameraControl>().Zoom(1f, 0.01f, poepZoom);
        yield return new WaitForSeconds(3.8f);
        InfoHolder.SetLastScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Poepen");
    }
}
