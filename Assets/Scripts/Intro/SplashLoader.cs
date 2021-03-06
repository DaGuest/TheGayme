﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashLoader : MonoBehaviour
{
    private ExitTo fader;
    public float timeToShow = 5;
    public bool lastSlide = false;
    public bool gameover = false;
    MasterController masterController;
    bool canControl = false;

    void Start()
    {
        fader = gameObject.GetComponent<ExitTo>();
        StartCoroutine(ToSplash());
        if (gameover)
        {
            masterController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterController>();
            Text text = GameObject.FindObjectOfType<Text>();
            string gameoverWaarde = "TOO MUCH " + ((masterController.gameOverTriggered) ? "POO!\n" : "GEILHEID!\n");
            long score = (long)Time.time;
            text.text = "GAME OVER\n" + gameoverWaarde + "SCORE: " + score;
        }
    }

    void Update()
    {
        if (canControl)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(fader.FadeOut());
            }
        }
    }


    IEnumerator ToSplash()
    {
        yield return new WaitForSeconds(timeToShow);
        if (!lastSlide)
        {
            StartCoroutine(fader.FadeOut());
        }
        else {
            canControl = true;
        }

    }
}
