using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashLoader : MonoBehaviour
{
    private ExitTo fader;
    public int timeToShow = 5;
    public bool lastSlide = false;
    public bool gameover = false;

    void Awake() {
        fader = gameObject.GetComponent<ExitTo>();
    }

    void Start() {
        if (!lastSlide) {
            StartCoroutine(ToSplash());
        }
        if (gameover) {
            Text text = GameObject.FindObjectOfType<Text>();
            MasterController masterController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterController>();
            string gameoverWaarde = "TOO MUCH" + ((masterController.poepWaarde == 100) ? "POO!\n" : "GEILHEID!\n");
            long score = (long)Time.time;
            text.text = "GAME OVER\n" + gameoverWaarde + "SCORE: " + score;
        }
    }

    void Update() {
        if (lastSlide) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartCoroutine(fader.FadeOut());
            }
        }
    }


    IEnumerator ToSplash() {
        yield return new WaitForSeconds(timeToShow);
        StartCoroutine(fader.FadeOut());
    }
}
