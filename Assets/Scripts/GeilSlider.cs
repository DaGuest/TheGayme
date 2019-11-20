using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeilSlider : MonoBehaviour
{
    public int geilWaarde = 0;
    [SerializeField] Sprite[] penisStates;
    Image penisImage;
    bool throbbing = false;
    
    void Awake() {
        penisImage = gameObject.GetComponent<Image>();
        geilWaarde = InfoHolder.GetGeilLevel();
        ChangeState();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            geilWaarde+=10;
            ChangeState();
        }   
        else if (Input.GetKeyDown(KeyCode.S)) {
            geilWaarde-=10;
            ChangeState();
        } 
    }

    void ChangeState() {
        int stateIndex = ((penisStates.Length * geilWaarde) / 100);
        if (stateIndex > 8) {
            throbbing = true;
            StopAllCoroutines();
            StartCoroutine(Throb());
        }
        else {
            throbbing = false;
            StopAllCoroutines();
            penisImage.sprite = penisStates[stateIndex];
        }
    }

    IEnumerator Throb() {
        while (throbbing) {
            penisImage.sprite = penisStates[9];
            yield return new WaitForSeconds(0.5f);
            penisImage.sprite = penisStates[8];
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ChangeGeilWaarde(int value) {
        geilWaarde = value;
        ChangeState();
    }
}
