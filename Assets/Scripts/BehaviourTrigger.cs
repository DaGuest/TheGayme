using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTrigger : MonoBehaviour {
	public int triggerBehaviour = 0;

	void OnTriggerEnter2D(Collider2D other) {
		if (triggerBehaviour == 0){
			other.GetComponent<Player>().SetPoepAble(true);
		}
		else if (triggerBehaviour == 1) {
			other.GetComponent<Player>().SetGamen(true);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (triggerBehaviour == 0){
			other.GetComponent<Player>().SetPoepAble(false);
		}
		else if (triggerBehaviour == 1) {
			other.GetComponent<Player>().SetGamen(false);
		}
	}
}
