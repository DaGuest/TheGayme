using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	float moveFactor = 0.1f;
	bool canMove;
	SpriteRenderer spriteRenderer = null;
	Animator anim = null;
	SpriteRenderer playerSpriteRenderer;
	bool poepAble = false;
	Vector3 poepPlek;
	bool battle;
	Sprite tegenstanderSprite;
	string tegenstanderNaam;

	public delegate void OnBattleReady(bool value, string text);
	public OnBattleReady onBattleReady;
	public delegate void OnBattle();
	public OnBattle onBattle;
	public delegate void OnPoepen();
	public OnPoepen onPoepen;

	void Awake() {
		canMove = true;
		battle = false;
	}

	void Start() {
		spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
		anim = gameObject.GetComponentInChildren<Animator>();
		playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		poepPlek = GameObject.FindGameObjectWithTag("poepplek").transform.position;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (poepAble) {
				poepenAan();
				canMove = false;
			}
			else if (battle) {
				onBattle();
				canMove = false;
			}
		}
	}

	public void Move() {
		if (canMove) {
			if (Input.GetKey(KeyCode.LeftArrow)) {
				if (spriteRenderer.flipX) {
					spriteRenderer.flipX = !spriteRenderer.flipX;
				}
				transform.Translate(-transform.right * moveFactor);
				anim.SetBool("walk", true);
			}
			else if (Input.GetKey(KeyCode.RightArrow)) {
				if (!spriteRenderer.flipX) {
					spriteRenderer.flipX = !spriteRenderer.flipX;
				}
				transform.Translate(transform.right * moveFactor);
				anim.SetBool("walk", true);
			}
			else if (Input.GetKey(KeyCode.DownArrow)) {
				anim.SetBool("walk", true);
				transform.Translate(-transform.up * moveFactor);
			}
			else if (Input.GetKey(KeyCode.UpArrow)) {
				anim.SetBool("walk", true);
				transform.Translate(transform.up * moveFactor);
			}
			else {
				anim.SetBool("walk", false);
			}
		}
	}

	//Nodig voor Huis scene
	public void SetLayer() {
		if (transform.position.y >= 2.9f) {
			playerSpriteRenderer.sortingOrder = 0;
		}
		else if (transform.position.y > -2.4f && transform.position.y < 2.9f) {
			playerSpriteRenderer.sortingOrder = 1;
		}
		else {
			playerSpriteRenderer.sortingOrder = 3;
		}
	}

	public void poepenAan() {
		transform.position = poepPlek;
		playerSpriteRenderer.flipX = false;
		anim.SetBool("poepen", true);
		canMove = false;
		onPoepen();
	}

	public void SetPoepAble(bool value) {
		poepAble = value;
		onBattleReady(value, "START POOPING!");
	}

	public void SetBattle(bool value) {
		battle = value;
		onBattleReady(value, "START FLIRTING!");
	}

	public void SetMoveable(bool value) {
		canMove = value;
	}
}
