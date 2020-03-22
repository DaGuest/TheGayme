using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    bool canFlicker = true;
    Light2D lt;
    float intensity;

    private void Start() {
        lt = gameObject.GetComponent<Light2D>();
        intensity = lt.intensity;
    }

    private void FixedUpdate() {
        if (canFlicker) {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker() {
        canFlicker = false;
        yield return new WaitForSeconds(Random.Range(1.5f, 5f));
        lt.intensity = Random.Range(0.05f, 0.5f);
        yield return new WaitForFixedUpdate();
        lt.intensity = intensity;
        canFlicker = true;
    }
}
