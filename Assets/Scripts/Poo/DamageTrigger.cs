using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ghost") {
            animator.Play("ui_hit");
        }
    }
}
