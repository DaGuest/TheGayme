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
    MasterController masterController;
    
    void Start() {
        masterController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterController>();
        penisImage = gameObject.GetComponent<Image>();
        geilWaarde = masterController.geilWaarde;
        ChangeState();
        SubscribeToBehaviours();
    }

    void SubscribeToBehaviours() {
        masterController.onWaardeChanged += ChangeGeilWaarde;
    }

    void UnSubscribeFromBehaviours() {
        masterController.onWaardeChanged -= ChangeGeilWaarde;
    }

    void ChangeState() {
        int stateIndex = ((penisStates.Length * geilWaarde) / 100);
        if (stateIndex == penisStates.Length - 1) {
            throbbing = true;
            StopAllCoroutines();
            StartCoroutine(Throb());
        }
        else if (stateIndex < penisStates.Length - 1) {
            throbbing = false;
            StopAllCoroutines();
            penisImage.sprite = penisStates[stateIndex];
        }
    }

    IEnumerator Throb() {
        while (throbbing) {
            penisImage.sprite = penisStates[penisStates.Length - 1];
            yield return new WaitForSeconds(0.5f);
            penisImage.sprite = penisStates[penisStates.Length - 2];
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ChangeGeilWaarde(int value, string waardeNaam) {
        if (waardeNaam.Equals("geil")) {
            geilWaarde = value;
            ChangeState();
        }
    }

    void OnDestroy() {
        UnSubscribeFromBehaviours();
    }
}
