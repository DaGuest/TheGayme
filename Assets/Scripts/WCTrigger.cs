using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WCTrigger : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		other.GetComponent<Player>().SetPoepAble(true);
	}

	void OnTriggerExit2D(Collider2D other) {
		other.GetComponent<Player>().SetPoepAble(false);
	}
}
