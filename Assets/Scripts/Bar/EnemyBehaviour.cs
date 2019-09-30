using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : NPCBehaviour {

	void OnTriggerEnter2D(Collider2D collision) {
		setWalking(false);
		InfoHolder.SetEnemyInfo(gameObject.GetComponent<CharInfo>());
		collision.GetComponent<Player>().SetBattle(true);
	}

	void OnTriggerExit2D(Collider2D collision) {
		setWalking(true);
		InfoHolder.SetEnemyInfo(null);
		collision.GetComponent<Player>().SetBattle(false);
	}
}
