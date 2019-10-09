using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poo : MonoBehaviour {

	public float lvlSpeed;
	public float moveSpeed;
	public int size = 0;

	public List<GameObject> obstacles;
	public Transform[] positions;

	float vDistanceTravelled = 0;
	float spawnRate = 25;

	Animator animator;
	Transform cam;
	bool canMove = true;

	void Start () {
		animator = gameObject.GetComponent<Animator>();
		cam = GameObject.Find ("Main Camera").GetComponent<Transform>();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			lvlSpeed += 1;
		}
		Move();
		ChangeSize();
		SpawnObstacles ();
	}

	void Move(){
		if (canMove) {
			Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			transform.position += move * moveSpeed * Time.deltaTime;
			transform.position = transform.position + Vector3.down * lvlSpeed * Time.deltaTime;
			cam.position = cam.position + Vector3.down * lvlSpeed * Time.deltaTime;
			vDistanceTravelled += lvlSpeed * Time.deltaTime;
		}
	}

	void ChangeSize() {
		if (size < 5) {
			animator.SetTrigger("klein");
		}
		else if (size < 10) {
			animator.SetTrigger("middel");
		}
		else {
			animator.SetTrigger("groot");
		}
	}

	void GameOver() {
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		rb.gravityScale = 3;
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		sr.sortingOrder = 2;
		BoxCollider2D bc = gameObject.GetComponent<BoxCollider2D>();
		bc.isTrigger = true;
	}

	void SpawnObstacles(){
		if (vDistanceTravelled > 1.8f) {
			int pos1 = Random.Range (0, positions.Length);
			int obstacle1 = Random.Range (0, obstacles.Count);
			int pos2 = Random.Range (0, positions.Length);
			while (pos1 == pos2) {
				pos2 = Random.Range (0, positions.Length);
			}
			int obstacle2 = Random.Range (0, obstacles.Count);
			if(Physics2D.OverlapBox (positions [pos1].position, Vector2.one, 0) == null && Random.Range(0,100) < spawnRate)
				GameObject.Instantiate (obstacles [obstacle1], positions [pos1].position, Quaternion.identity);
			if(Physics2D.OverlapBox (positions [pos2].position, Vector2.one, 0) == null && Random.Range(0,100) < spawnRate)
				GameObject.Instantiate (obstacles [obstacle2], positions [pos2].position, Quaternion.identity);
			vDistanceTravelled = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Gas") {
			lvlSpeed += .75f;
			GameObject.Destroy (col.gameObject);
		}

		if (col.tag == "Poo") {
			lvlSpeed -= .5f;
			size++;
			GameObject.Destroy (col.gameObject);
		}

		if (col.tag == "TopWall") {
			canMove = false;
			GameOver();
		}
	}
}
