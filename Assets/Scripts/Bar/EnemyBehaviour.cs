using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : NPCBehaviour {
	private bool originalFlip;
	private CharInfo charInfo;

	void Awake() {
		if (InfoHolder.playerInfoLoaded && InfoHolder.GetEnemyInfo().GetNaam().Equals(gameObject.name)) {
			charInfo = InfoHolder.GetEnemyInfo();
		}
		else {
			charInfo = gameObject.GetComponent<CharInfo>();
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (charInfo.isFlirtable) {
			setWalking(false);
			originalFlip = gameObject.GetComponent<SpriteRenderer>().flipX;
			gameObject.GetComponent<SpriteRenderer>().flipX = !collision.GetComponent<SpriteRenderer>().flipX;
			InfoHolder.SetEnemyInfo(charInfo);
			collision.GetComponent<Player>().SetBattle(true);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (charInfo.isFlirtable) {
			setWalking(true);
			gameObject.GetComponent<SpriteRenderer>().flipX = originalFlip;
			InfoHolder.SetEnemyInfo(null);
			collision.GetComponent<Player>().SetBattle(false);
		}
	}
}