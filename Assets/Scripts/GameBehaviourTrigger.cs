using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviourTrigger : MonoBehaviour
{
        void OnTriggerEnter2D(Collider2D other) {
                other.GetComponent<Player>().SetGamen(true);
        }       

	void OnTriggerExit2D(Collider2D other) {
                other.GetComponent<Player>().SetGamen(false);
	}
}
