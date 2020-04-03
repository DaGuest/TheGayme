using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterController : MonoBehaviour
{
    private static bool created = false;

    public delegate void OnWaardeChanged(int nieuweWaarde, string waardeNaam);
    public OnWaardeChanged onWaardeChanged;
    
    public int geilWaarde;
    public int poepWaarde;
    public int counter = 0;
    public int difficultyCounter = 0;
    public float waitTime = 1f;
    bool canCount = false;
    MusicManager musicManager;
    
    void Awake() {
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else {
            GameObject.Destroy(this.gameObject);
        }
    }

    void Start() {
        musicManager = gameObject.GetComponent<MusicManager>();
        StartCoroutine(Timer());
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        canCount = false;
        if (!scene.name.Equals("Versier") && !scene.name.Equals("Map") && !scene.name.Equals("SplashTitle") && !scene.name.Equals("Poepen") && !scene.name.Equals("GameOver")) {
            canCount = true;
        }
        musicManager.PlaySceneSong(scene.name);
    }

    IEnumerator Timer() {
        while (true) {
            yield return new WaitForSeconds(waitTime);
            if (canCount) {
                counter++;
                if (counter == 15 || counter == 45) {
                    SetGeilWaarde(10);
                }
                else if (counter == 25) {
                    SetPoepWaarde(15);
                }
                else if (counter > 60) {
                    counter = 0;
                    difficultyCounter++;
                    if (geilWaarde >= 100 || poepWaarde >= 100) {
                        ResetValues();
                        SceneManager.LoadScene("GameOver");
                    }
                    if (difficultyCounter >= 2) {
                        difficultyCounter = 0;
                        waitTime *= .8f;
                    }
                }   
            }
        }
        
    }

    void ResetValues() {
        InfoHolder.ResetValues();
        geilWaarde = 50;
        poepWaarde = 50;
        counter = 0;
    }

    public void SetGeilWaarde(int valueToAdd) {
        geilWaarde = GetCorrectSliderValue(geilWaarde, valueToAdd);
        if (onWaardeChanged != null) {
            onWaardeChanged(geilWaarde, "geil");
        }
    }

    public void SetPoepWaarde(int valueToAdd) {
        poepWaarde = GetCorrectSliderValue(poepWaarde, valueToAdd);
        if (onWaardeChanged != null) {
            onWaardeChanged(poepWaarde, "poep");
        }
    }

    public int GetWaarde(string waardeNaam) {
        int returnWaarde = 0;
        if (waardeNaam.Equals("poep")) {
            returnWaarde = poepWaarde;
        }
        else if (waardeNaam.Equals("geil")) {
            returnWaarde = geilWaarde;
        }
        return returnWaarde;
    }

    private int GetCorrectSliderValue(int origValue, int valueToAdd) {
        int newValue = origValue + valueToAdd;
        return Mathf.Max(0, Mathf.Min(100, newValue));
    }
}
