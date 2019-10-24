using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poo : MonoBehaviour {
	public float lvlSpeed;

	public List<GameObject> obstacles;
	public Transform[] positions;
	public PooPlayer player;
	public CameraControls camControls;

	float vDistanceTravelled = 0;
	float spawnRate = 25;
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			lvlSpeed += 1;
		}
		if (camControls.GetY() < -220) {
			lvlSpeed = 0;
		}
		SpawnObstacles();
		camControls.Move(lvlSpeed);
		player.Move(lvlSpeed);
		vDistanceTravelled += lvlSpeed * Time.deltaTime;
	}

	void GameOver() {
		lvlSpeed = 0;
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

}
