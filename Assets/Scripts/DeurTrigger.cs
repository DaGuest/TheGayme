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
		deurPost.sortingLayerName = "VoorPlayer";
		badkamer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
	}

	void OnTriggerExit2D(Collider2D other) {
		deurPost.sortingLayerName = "Background";
		deurOpen = false;
		badkamer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
	}
}
