using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lichten : MonoBehaviour {
	public GameObject licht;

	void Start() {
		StartCoroutine(maakLichten());
	}

	IEnumerator maakLichten() {
		while(true) {
			if (gameObject.GetComponentsInChildren<LichtBehaviour>().Length < 20) {
				Vector2 lichtPos = new Vector2(Random.Range(-20f, 20f), Random.Range(-13f, 3f));
				GameObject nieuwLicht = Instantiate(licht, lichtPos, Quaternion.identity);
				nieuwLicht.transform.SetParent(gameObject.transform);
				yield return new WaitForSeconds(0.3f);
			}
		}
	}

}
