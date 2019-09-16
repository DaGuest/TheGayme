﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public Transform playerTransform;
	public float leftBorder = 1.11f;
	public float rightBorder = 8.5f;
	public float upBorder = 3;
	public float downBorder = 0;
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move() {
		Vector3 camPosition = transform.position;
		if (playerTransform.position.x > leftBorder && playerTransform.position.x < rightBorder) {
			camPosition.x = playerTransform.position.x;
		}
		if (playerTransform.position.y > downBorder && playerTransform.position.y < upBorder) {
			camPosition.y = playerTransform.position.y;
		}
		transform.position = camPosition;
	}

	public void Zoom(float sec, float targetSize, Vector3 target) {
		StartCoroutine(ZoomIn(sec, targetSize, target));
	}

	IEnumerator ZoomIn(float sec, float targetSize, Vector3 target) {
		yield return new WaitForSeconds(2.5f);
		float sizeChange = Camera.main.orthographicSize - targetSize; 
		float zoomDelta = (sizeChange / (sec / 0.01f)) * 1.65f;
		float moveRate = (Vector3.Distance(transform.position, target) / ((sec / 0.01f))) * 1.8f;
		while(Camera.main.orthographicSize > targetSize) {
			Camera.main.orthographicSize -= zoomDelta;
			transform.position = Vector3.MoveTowards(transform.position, target, moveRate);
			yield return new WaitForSeconds(0.01f);
		}
	}
}
