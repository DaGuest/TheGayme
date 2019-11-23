using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Audio;

public class MasterController : MonoBehaviour
{
    private static bool created = false;

    public delegate void OnWaardeChanged(int nieuweWaarde, string waardeNaam);
    public OnWaardeChanged onWaardeChanged;
    
    public int geilWaarde;
    public int poepWaarde;
    public int counter = 0;
    bool canCount = false;
    
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
        StartCoroutine(Timer());
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        canCount = false;
        if (!scene.name.Equals("Versier") && !scene.name.Equals("Map") && !scene.name.Equals("SplashTitle")) {
            canCount = true;
        }
    }

    IEnumerator Timer() {
        while (true) {
            yield return new WaitForSeconds(1);
            if (canCount) {
                counter++;
                if (counter == 15) {
                    SetGeilWaarde(10);
                }
                else if (counter == 25) {
                    SetPoepWaarde(10);
                }
                else if (counter > 60) {
                    counter = 0;
                    if (geilWaarde == 100 || poepWaarde == 100) {
                        break;
                    }
                }   
            }
        }
        SceneManager.LoadScene("GameOver");
    }

    public void SetGeilWaarde(int valueToAdd) {
        geilWaarde = GetCorrectSliderValue(geilWaarde, valueToAdd);
        onWaardeChanged(geilWaarde, "geil");
    }

    public void SetPoepWaarde(int valueToAdd) {
        poepWaarde = GetCorrectSliderValue(poepWaarde, valueToAdd);
        onWaardeChanged(poepWaarde, "poep");
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
