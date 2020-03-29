using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{ 
    public AudioClip[] musicClips;
    private AudioSource audioSource;
    private int currentTrack = 0;
    bool loadingTrack = false;
    bool loadNext = false;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySceneSong(string sceneName) {
        switch (sceneName) {
            case "Versier":
                loadNext = true;
                currentTrack = 1;
                break;
            case "Poepen":
                loadNext = true;
                currentTrack = 2;
                break;
            case "SplashProd":
            case "SplashTitle":
                if (currentTrack < 3) {
                    loadNext = true;
                    currentTrack = 3;
                }
                break;
            default:
                if (currentTrack > 0) {
                    loadNext = true;
                    currentTrack = 0;
                }
                break;
        }
        if (loadNext) {
            StartCoroutine(LoadNext());
            loadNext = false;
        }
    }

    IEnumerator LoadNext() {
        //Fade out
        while (audioSource.volume > 0) {
            audioSource.volume -= 0.015f;
            yield return new WaitForFixedUpdate();
        }
        //Load next song
        loadingTrack = true;
        audioSource.clip = musicClips[currentTrack];
        audioSource.Play();
        //Fade in
        while (audioSource.volume < 0.5f) {
            audioSource.volume += 0.01f;
            yield return new WaitForFixedUpdate();
        }
        loadingTrack = false;
    }
}
