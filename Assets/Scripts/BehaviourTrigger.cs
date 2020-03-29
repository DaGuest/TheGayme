using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTrigger : MonoBehaviour {
	public string triggerBehaviour;

	void OnTriggerEnter2D(Collider2D other) {
		switch (triggerBehaviour) {
			case "poepen":
			other.GetComponent<Player>().SetPoepAble(true);
			break;
			case "gamen":
			other.GetComponent<Player>().SetGamen(true);
			break;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		switch (triggerBehaviour) {
			case "poepen":
			other.GetComponent<Player>().SetPoepAble(false);
			break;
			case "gamen":
			other.GetComponent<Player>().SetGamen(false);
			break;
		}
	}
}
