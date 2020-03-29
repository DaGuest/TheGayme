using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class HuisController : MonoBehaviour
{
    Vector3 poepZoom;
    public Player player;
    public TrapLayers trap;
    public Text spacebarText;
    public Transform gamePosition;
    public Animator animator;
    public Light2D tvLight;
    bool canMove = true;
    Vector2 currentPosition;

    void Start() {
        poepZoom = GameObject.FindGameObjectWithTag("poepzoom").transform.position;
        SubScribeToBehaviours();
    }

    void FixedUpdate() {
        player.Move();
        trap.SetLayer(player.transform.position.y);
    }

    void SubScribeToBehaviours() {
        player.onBattleReady += BattleReady;
        player.onPoepen += StartPoepen;
        player.onGamen += StartGamen;
    }

    void BattleReady(bool value, string text) {
        if (value) {
            SetSpaceBarText(text, Color.black, 21);
        }
        else {
            SetSpaceBarText("SPACEBAR", Color.grey, 19);
        }
    }

    void SetSpaceBarText(string text, Color color, int size) {
        spacebarText.text = text;
        spacebarText.color = color;
        spacebarText.fontSize = size;
    }

    void StartPoepen() {
        InfoHolder.SetBarPosition(new Vector3(19.23f, 4.72f, 0));
        StartCoroutine(IStartPoepen());
    }

    IEnumerator IStartPoepen() {
        Camera.main.GetComponent<CameraControl>().Zoom(1f, 0.01f, poepZoom);
        yield return new WaitForSeconds(3.8f);
        InfoHolder.SetLastScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Poepen");
    }

    void StartGamen() {
        player.SetMoveable(false);
        currentPosition = player.transform.position;
        player.transform.position = gamePosition.position;
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        StartCoroutine(Gamen());
    }

    IEnumerator Gamen() {
        while (tvLight.intensity < 0.5f) {
            tvLight.intensity += 0.05f;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("fadeout");
        yield return new WaitForSeconds(5f);
        player.transform.position = currentPosition;
        player.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        player.SetMoveable(true);
        while (tvLight.intensity > 0) {
            tvLight.intensity -= 0.05f;
            yield return new WaitForFixedUpdate();
        }
    }
}
