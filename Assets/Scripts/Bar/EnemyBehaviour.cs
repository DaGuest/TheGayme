using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : NPCBehaviour {
	private CharInfo charInfo;

	void Awake() {
		if (InfoHolder.playerInfoLoaded && InfoHolder.GetLastScene().Equals("Versier") && InfoHolder.GetEnemyInfo().GetNaam().Equals(gameObject.name)) {
			charInfo = InfoHolder.GetEnemyInfo();
		}
		else {
			charInfo = gameObject.GetComponent<CharInfo>();
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (charInfo.isFlirtable) {
			SetWalking(false);
			InfoHolder.SetEnemyInfo(charInfo);
			collision.GetComponent<Player>().SetBattle(true);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (charInfo.isFlirtable) {
			SetWalking(true);
			InfoHolder.SetEnemyInfo(null);
			collision.GetComponent<Player>().SetBattle(false);
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		Vector2 direction = transform.position - other.transform.position;
		gameObject.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
	}
}