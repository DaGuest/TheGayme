using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeurTrigger : MonoBehaviour {
	public bool deurOpen = false;
	public SpriteRenderer badkamer;
	public SpriteRenderer deurPost;

	void Start() {
		deurPost = gameObject.GetComponentInChildren<SpriteRenderer>();
	}

	void OnTriggerStay2D(Collider2D other) {
		deurOpen = true;
		deurPost.sortingOrder = 2;
		badkamer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
	}

	void OnTriggerExit2D(Collider2D other) {
		deurPost.sortingOrder = -1;
		deurOpen = false;
		badkamer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
	}
}
