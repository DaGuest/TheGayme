using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTrigger : MonoBehaviour {
	public int triggerBehaviour = 0;

	void OnTriggerEnter2D(Collider2D other) {
		switch (triggerBehaviour) {
			case 0:
				other.GetComponent<Player>().SetPoepAble(true);
				break;
			case 1:
				other.GetComponent<Player>().SetGamen(true);
				break;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		switch (triggerBehaviour) {
			case 0:
				other.GetComponent<Player>().SetPoepAble(false);
				break;
			case 1:
				other.GetComponent<Player>().SetGamen(false);
				break;
		}
	}
}
