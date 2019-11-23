using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    MasterController masterController;
    Slider slider;
    Animator animator;
    public string waardeNaam;

    void Awake() {
        masterController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterController>();
        slider = gameObject.GetComponent<Slider>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Start() {
        slider.value = masterController.GetWaarde(waardeNaam);
        SubscribeToBehaviours();
    }

    void SubscribeToBehaviours() {
        masterController.onWaardeChanged += ChangeSliderValue;
    }

    void UnSubscribeFromBehaviours() {
        masterController.onWaardeChanged -= ChangeSliderValue;
    }

    void ChangeSliderValue(int nieuweWaarde, string value) {
        if (waardeNaam.Equals(value)) {
            slider.value = nieuweWaarde;
            if (slider.value < 90) {
                StopAllCoroutines();
                animator.SetTrigger("valueUp");
            }
            else {
                StopAllCoroutines();
                StartCoroutine(LoopChange());
            }
        }
    }

    IEnumerator LoopChange() {
        while(true) {
            yield return new WaitForSeconds(1);
            animator.SetTrigger("valueUp");
        }
    }

    void OnDestroy() {
        UnSubscribeFromBehaviours();
    }
}
