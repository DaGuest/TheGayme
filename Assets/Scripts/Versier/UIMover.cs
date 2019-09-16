using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour {
	public Vector3 goalPosition;
	public float speed;

	public void Show() {
		gameObject.SetActive(true);
	}

	public void PerformAttackMove() {
		StartCoroutine(MoveBackAndForth());
	}

	IEnumerator MoveBackAndForth() {
		Vector3 originalPosition = transform.localPosition;
		Vector3 rightPosition = originalPosition + (transform.right * 3);
		Debug.Log(rightPosition + " - " + originalPosition);
		while (Vector3.Distance(transform.localPosition, rightPosition) > 0.1f) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, rightPosition, 1.0f);
			yield return new WaitForFixedUpdate();
		}
		while (Vector3.Distance(transform.localPosition, originalPosition) > 0.1f) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, 0.7f);
			yield return new WaitForFixedUpdate();
		}
	}
}
