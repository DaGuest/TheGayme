using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	float factor = 0.1f;
	bool canMove;
	SpriteRenderer sr = null;
	Animator anim = null;
	SpriteRenderer playerSpriteRenderer;
	bool poepAble = false;
	public int sgraalheid = 50;
	Vector3 poepPlek;
	Vector3 poepZoom;
	bool jager;
	bool battle;
	Sprite tegenstanderSprite;
	string tegenstanderNaam;

	public delegate void SgraalheidAction(int sgraalheidAanpassing, int sgraalheid);
	public static event SgraalheidAction OnSgraalheid;
	public delegate void OnBattle();
	public OnBattle onBattle;

	void Awake() {
		canMove = true;
		jager = false;
		battle = false;
	}

	void Start() {
		sr = gameObject.GetComponentInChildren<SpriteRenderer>();
		anim = gameObject.GetComponentInChildren<Animator>();
		playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		poepPlek = GameObject.FindGameObjectWithTag("poepplek").transform.position;
		poepZoom = GameObject.FindGameObjectWithTag("poepzoom").transform.position;
	}

	void FixedUpdate() {
		if (canMove) {
			move();
			setLayer();
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			if (poepAble) {
				poepenUit();
			}
			setSgraalheid(-5);
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (poepAble) {
				poepenAan();
			}
			else if (battle) {
				onBattle();
			}
		}
		if (Input.GetKeyDown(KeyCode.J)) {
			setJager(!jager);
		}
	}

	void move() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			if (sr.flipX) {
				sr.flipX = !sr.flipX;
			}
			transform.Translate(-transform.right * factor);
			anim.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			if (!sr.flipX) {
				sr.flipX = !sr.flipX;
			}
			transform.Translate(transform.right * factor);
			anim.SetBool("walk", true);
		}
		else if (Input.GetKey(KeyCode.DownArrow)) {
			anim.SetBool("walk", true);
			transform.Translate(-transform.up * factor);
		}
		else if (Input.GetKey(KeyCode.UpArrow)) {
			anim.SetBool("walk", true);
			transform.Translate(transform.up * factor);
		}
		else {
			anim.SetBool("walk", false);
		}
	}

	//Nodig voor Huis scene
	void setLayer() {
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
		Camera.main.GetComponent<CameraControl>().Zoom(1f, 0.01f, poepZoom);
	}

	public void poepenUit() {
		transform.position = poepPlek - new Vector3(0.12f, 0.63f, 0);
		anim.SetBool("poepen", false);
		canMove = true;
	}
	public void setPoepAble(bool value) {
		poepAble = value;
	}

	public void SetBattle(bool value) {
		battle = value;
	}

	public int getSgraalheid() {
		return sgraalheid;
	}

	public void setSgraalheid(float valueToAdd) {
		sgraalheid = (int)Mathf.Clamp(sgraalheid + valueToAdd, 0, 100);
		if (OnSgraalheid != null) {
			OnSgraalheid((int)valueToAdd, sgraalheid);
		}
	}

	public void setJager(bool value) {
		jager = value;
		anim.SetBool("jager", jager);
		if (jager) {
			StartCoroutine(jagerCounter());
		}
	}

	IEnumerator jagerCounter() {
		yield return new WaitForSeconds(10f);
		setJager(false);
	}
}
