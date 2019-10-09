using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTo : MonoBehaviour {
    public Animator anim;
    public string sceneToLoad;

    void OnTriggerEnter2D(Collider2D other) {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null) {
            player.SetMoveable(false);
        }
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut() {
        anim.SetTrigger("fadeout");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
