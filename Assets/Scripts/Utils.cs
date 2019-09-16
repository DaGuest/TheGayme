using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {
	public static Vector2 getRandomMoveDirection() {
		Vector2 moveDirection = Vector2.zero;
		while (moveDirection.Equals(Vector2.zero)) {
			moveDirection = new Vector2(Random.Range(-1,2), Random.Range(-1,2));
		}
		return moveDirection;
	}
}
