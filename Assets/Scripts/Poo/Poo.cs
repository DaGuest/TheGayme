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

	public bool touchingTopWall = false;

	Transform cam;

	void Start () {
		cam = GameObject.Find ("Main Camera").GetComponent<Transform> ();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			lvlSpeed += 1;
		}

		Move ();

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * moveSpeed * Time.deltaTime;

		SpawnObstacles ();
	}

	void Move(){
		transform.position = transform.position + Vector3.down * lvlSpeed * Time.deltaTime;
		cam.position = cam.position + Vector3.down * lvlSpeed * Time.deltaTime;
		vDistanceTravelled += lvlSpeed * Time.deltaTime;
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
			touchingTopWall = true;
		}

		if (col.tag == "Wall") {
			if (touchingTopWall == true)
				Debug.Log ("Dead");
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "TopWall") {
			touchingTopWall = false;
		}
	}
}
