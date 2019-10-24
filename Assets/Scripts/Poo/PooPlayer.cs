using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooPlayer : MonoBehaviour {
    float size = 0;
    float moveSpeed = 5;
    Animator animator;
    SpriteRenderer body;
    public SpriteRenderer face;

    void Start() {
        animator = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        ChangeSize();
        FlipFace();
    }

    public void Move(float lvlSpeed){
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;
        transform.position = transform.position + Vector3.down * lvlSpeed * Time.deltaTime;
	}

    void ChangeSize() {
		if (size < 5) {
			animator.SetBool("klein", true);
			animator.SetBool("middel", false);
			animator.SetBool("groot", false);
		}
		else if (size < 10) {
			animator.SetBool("klein", false);
			animator.SetBool("middel", true);
			animator.SetBool("groot", false);
		}
		else {
			animator.SetBool("klein", false);
			animator.SetBool("middel", false);
			animator.SetBool("groot", true);
		}
	}

    void FlipFace() {
		if (Input.GetAxis("Horizontal") > 0) {
				body.flipX = true;
				face.flipX = true;
			}
		else if (Input.GetAxis("Horizontal") < 0){
			body.flipX = false;
			face.flipX = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Gas") {
			//lvlSpeed += .75f;
			//moveSpeed += .5f;
			GameObject.Destroy (col.gameObject);
			animator.SetTrigger("chew");
		}

		if (col.tag == "Poo") {
			//moveSpeed -= .25f;
			size++;
			GameObject.Destroy (col.gameObject);
			animator.SetTrigger("chew");
		}

		if (col.tag == "TopWall") {
			//GameOver();
		}
	}
}
