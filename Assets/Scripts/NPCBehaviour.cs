using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour {
    private Vector2 startPosition;
    public float walkBoundary = 1.5f;
    private Vector2 moveDirection = Vector2.right;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private int pauseCounter = 100;
    private bool walking = true;

	void Start() {
        startPosition = transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update() {
        move();
    }

    void move() {
        if (!walking) {
            animator.SetBool("Walk", false);
        }
        else if (Vector2.Distance(transform.position, startPosition) > walkBoundary) {
            if (animator.GetBool("Walk") == true) {
                animator.SetBool("Walk", false);
            }
            pauseCounter -= 1;
            if (pauseCounter == 0) {
                moveDirection *= -1;
                spriteRenderer.flipX = !spriteRenderer.flipX;
                pauseCounter = 100;
                transform.Translate(moveDirection * 0.05f);
                animator.SetBool("Walk", true);
            }
        }
        else {
            transform.Translate(moveDirection * 0.05f);
        }
    }

    protected void setWalking(bool value) {
        walking = value;
    }
}