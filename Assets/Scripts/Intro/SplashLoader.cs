using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
    public int timeToShow = 5;
    private ExitTo fader;

    void Awake() {
        fader = gameObject.GetComponent<ExitTo>();
    }

    void Start() {
        StartCoroutine(ToSplash());
    }

    IEnumerator ToSplash() {
        yield return new WaitForSeconds(timeToShow);
        StartCoroutine(fader.FadeOut());
    }
}
