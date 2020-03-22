using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenText : MonoBehaviour
{
    public GameObject texts;
    public GameObject spaceBarUI;

    void Start()
    {
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 3; i++)
        {
            texts.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            texts.SetActive(true);
            yield return new WaitForSeconds(0.7f);
        }
        texts.SetActive(false);
        spaceBarUI.SetActive(true);
    }
}
