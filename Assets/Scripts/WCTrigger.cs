using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WCTrigger : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		other.GetComponent<Player>().setPoepAble(true);
	}

	void OnTriggerExit2D(Collider2D other) {
		other.GetComponent<Player>().setPoepAble(false);
	}
}
