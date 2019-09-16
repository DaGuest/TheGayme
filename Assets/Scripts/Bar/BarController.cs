using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarController : MonoBehaviour {
    public Animator battleAnimator;
    public Player player;
	
    void Start() {
        SubScribeToBehaviours();
    }

    void SubScribeToBehaviours() {
        player.onBattle += StartBattle;
    }

    public void StartBattle() {
        StartCoroutine(IStartBattle());
    }

    IEnumerator IStartBattle() {
        battleAnimator.SetTrigger("battle");
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(1);
    }
}
