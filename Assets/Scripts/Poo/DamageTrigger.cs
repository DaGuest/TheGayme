using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ghost") {
            other.GetComponent<Ghost>().hitParticles.Play();
        }
    }
}
