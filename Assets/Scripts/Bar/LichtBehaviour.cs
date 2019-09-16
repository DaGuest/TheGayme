using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichtBehaviour : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	float addUnit = 0.02f;

	void Awake() {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), 0.1f);
	}

	void FixedUpdate() {
		Color col = spriteRenderer.color;
		if (col.a < 0) {
			Destroy(gameObject);
		}
		if (col.a > 0.5f) {
			addUnit *= -1;
		}
		col.a += addUnit;
		spriteRenderer.color = col;
	}
}
