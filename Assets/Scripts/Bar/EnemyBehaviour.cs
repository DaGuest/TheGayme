using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : NPCBehaviour {
	Sprite tegenstanderSprite;

	void Awake() {
		tegenstanderSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		setWalking(false);
		InfoHolder.SetTegenstanderNaam(gameObject.name);
		InfoHolder.SetTegenstanderSprite(tegenstanderSprite);
		collision.GetComponent<Player>().SetBattle(true);
	}

	void OnTriggerExit2D(Collider2D collision) {
		setWalking(true);
		InfoHolder.SetTegenstanderNaam("");
		InfoHolder.SetTegenstanderSprite(null);
		collision.GetComponent<Player>().SetBattle(false);
	}
}
