using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarController : MonoBehaviour {
    public Animator battleAnimator;
    public Player player;

    void Awake() {
        Transform playerTransform = player.GetComponent<Transform>();
        Vector3 startPosition = InfoHolder.GetBarPosition();
        if (startPosition != Vector3.zero) {
            playerTransform.position = startPosition;
        }
    }
	
    void Start() {
        InfoHolder.SetPlayerInfo(player.GetComponent<CharInfo>());
        SubScribeToBehaviours();
    }

    void SubScribeToBehaviours() {
        player.onBattle += StartBattle;
    }

    public void StartBattle() {
        InfoHolder.SetBarPosition(player.GetComponent<Transform>().position);
        StartCoroutine(IStartBattle());
    }

    IEnumerator IStartBattle() {
        battleAnimator.SetTrigger("battle");
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("Versier");
    }
}
