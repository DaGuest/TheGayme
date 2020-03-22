using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicClips;
    private AudioSource audioSource;
    private int currentTrack = 0;
    bool loadingTrack = false;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!audioSource.isPlaying && !loadingTrack) {
            PlayNext();
        }
    }

    public void PlayNext() {
        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext() {
        //Fade out
        while (audioSource.volume > 0) {
            audioSource.volume -= 0.005f;
            yield return new WaitForFixedUpdate();
        }
        //Load next song
        loadingTrack = true;
        audioSource.clip = musicClips[currentTrack];
        currentTrack = (currentTrack + 1) % musicClips.Length;
        audioSource.Play();
        //Fade in
        while (audioSource.volume < 0.5f) {
            audioSource.volume += 0.005f;
            yield return new WaitForFixedUpdate();
        }
        loadingTrack = false;
    }
}
