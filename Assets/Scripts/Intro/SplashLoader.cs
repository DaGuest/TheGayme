using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
    private ExitTo fader;
    public int timeToShow = 5;
    public bool lastSlide = false;

    void Awake() {
        fader = gameObject.GetComponent<ExitTo>();
    }

    void Start() {
        if (!lastSlide) {
            StartCoroutine(ToSplash());
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
