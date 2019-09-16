using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour {
	public delegate void OnTekstComplete();
	public event OnTekstComplete onTekstComplete;

	delegate void OnConfirm();

	public Text tekstObject;
	string tekst;
	Queue<string> messageQueue = new Queue<string>();
	bool scrollComplete = false;

	public void ShowQueuedTekst() {
		StartCoroutine(ShowMessages());
	}

	IEnumerator ShowMessages() {
		yield return new WaitForFixedUpdate();
		while (messageQueue.Count > 0) {
			tekst = messageQueue.Dequeue();
			StartCoroutine(ScrollTekst());
			yield return new WaitUntil(() => scrollComplete);
			scrollComplete = false;
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
			yield return new WaitForFixedUpdate();
		}
		if (onTekstComplete != null) {
			onTekstComplete();
		}
	}

	public void ShowTekst(string tekstToShow) {
		tekst = tekstToShow;
		StartCoroutine(ScrollTekst());
	}

	IEnumerator ScrollTekst() {
		tekstObject.text = "";
		for (int i = 0; i < tekst.Length; i++) {
			tekstObject.text += tekst[i];
			yield return new WaitForFixedUpdate();
		}
		scrollComplete = true;
	}
	
	public void ShowNoTekst() {
		tekstObject.text = "";
	}

	public void QueueMessage(string message) {
		messageQueue.Enqueue(message);
	}
}
