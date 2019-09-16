using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AanwijzerControl : MonoBehaviour {
	float maxRotation = 30;
	public Image hoofdImage;
	public Sprite[] hoofden;

	void OnEnable() {
		Player.OnSgraalheid += Aanwijzer;
	}

	void OnDisable() {
		Player.OnSgraalheid -= Aanwijzer;
	}

	void Aanwijzer(int rotationAmount, int sgraalheid) {
		float zRotation;
		zRotation = ((float)rotationAmount / 50) * maxRotation;
		transform.Rotate(0,0,zRotation);
		if (sgraalheid > 85 || sgraalheid < 15) {
			hoofdImage.sprite = hoofden[2];
		}
		else if (sgraalheid >= 65 || sgraalheid <= 35) {
			hoofdImage.sprite = hoofden[1];
		}
		else {
			hoofdImage.sprite = hoofden[0];
		}
	}
}
