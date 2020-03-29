using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    private Vector2 startPosition;
    public float walkBoundary = 1.5f;
    public Vector2 moveDirection = Vector2.right;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool stop, walking = true;
    private float moveSpeed = 2f;
    public bool canWalk = true;

    private void Start()
    {
        startPosition = transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (canWalk) {
            animator.SetBool("Walk", walking);
            if (walking)
            {
                if (Vector2.Distance(transform.position, startPosition) > walkBoundary)
                {
                    walking = false;
                    StartCoroutine(PauseWalk());
                }
                spriteRenderer.flipX = moveDirection.x > 0;
                transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
            }
        }
    }

    IEnumerator PauseWalk()
    {
        yield return new WaitForSeconds(1f);
        moveDirection *= -1;
        startPosition = transform.position;
        walking = true;
    }

    protected void SetWalking(bool value)
    {
        walking = value;
        StopAllCoroutines();
    }
}