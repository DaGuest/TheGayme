using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : NPCBehaviour {
	private bool originalFlip;

	void OnTriggerEnter2D(Collider2D collision) {
		setWalking(false);
		originalFlip = gameObject.GetComponent<SpriteRenderer>().flipX;
		gameObject.GetComponent<SpriteRenderer>().flipX = !collision.GetComponent<SpriteRenderer>().flipX;
		InfoHolder.SetEnemyInfo(gameObject.GetComponent<CharInfo>());
		collision.GetComponent<Player>().SetBattle(true);
	}

	void OnTriggerExit2D(Collider2D collision) {
		setWalking(true);
		gameObject.GetComponent<SpriteRenderer>().flipX = originalFlip;
		InfoHolder.SetEnemyInfo(null);
		collision.GetComponent<Player>().SetBattle(false);
	}
}
