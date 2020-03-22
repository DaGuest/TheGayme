using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    public float speed = 5;
    public float parallexEffect;
    private float width, startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (PooController.ghostCount == 5) {
            speed = 7.5f;
        }
        else if (PooController.ghostCount == 10) {
            speed = 10;
        }
        else if (PooController.ghostCount == 15) {
            speed = 15f;
        }
        else if (PooController.ghostCount == 20) {
            speed = 25f;
        }
        transform.Translate(-Vector2.right * Time.deltaTime * speed * (1-parallexEffect));
        if (transform.position.x < -width) {
            Reposition();
        }
    }

    void Reposition() {
        Vector2 newPos = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + newPos;
    }
}
