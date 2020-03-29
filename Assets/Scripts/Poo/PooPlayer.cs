using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    SpriteRenderer spriteRenderer;
    bool faceLeft = true;

    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Move() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        FlipPlayer();
        animator.SetFloat("Speed", movement.sqrMagnitude);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void FlipPlayer() {
        if (!faceLeft && movement.x < 0) {
            transform.eulerAngles = new Vector3(0,0,0);
            faceLeft = true;
        }
        else if (faceLeft && movement.x > 0) {
            transform.eulerAngles = new Vector3(0,180,0);
            faceLeft = false;
        }
    }
}
