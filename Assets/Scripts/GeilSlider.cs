using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeilSlider : MonoBehaviour
{
    public int geilWaarde = 0;
    [SerializeField] Sprite[] penisStates;
    Image penisImage;
    
    void Awake() {
        penisImage = gameObject.GetComponent<Image>();
        geilWaarde = InfoHolder.GetGeilLevel();
        ChangeState();
    }

    void ChangeState() {
        int stateIndex = ((penisStates.Length * geilWaarde) / 100);
        penisImage.sprite = penisStates[stateIndex];
    }

    public void ChangeGeilWaarde(int value) {
        geilWaarde = value;
        ChangeState();
    }
}
