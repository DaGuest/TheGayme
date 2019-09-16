using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarmanBehaviour : NPCBehaviour {
	bool jagerGiven = false;

	void OnTriggerEnter2D(Collider2D other) {
		setWalking(false);
		jagerGiven = true;
		GameObject.Find("Tekst").GetComponent<SpriteRenderer>().enabled = true;
		other.GetComponent<Player>().setJager(true);
	}

	void OnTriggerExit2D(Collider2D other) {
		setWalking(true);
		GameObject.Find("Tekst").GetComponent<SpriteRenderer>().enabled = false;
	}
}
