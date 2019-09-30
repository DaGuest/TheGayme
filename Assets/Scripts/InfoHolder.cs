using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoHolder {
	static CharInfo player;
	static CharInfo tegenstander;
	static Vector3 mapPosition;
	static Vector3 barPosition;

	public static void SetPlayerInfo(CharInfo playerInfo) {
		player = playerInfo;
	} 

	public static CharInfo GetPlayerInfo() {
		return player;
	}
	
	public static void SetEnemyInfo(CharInfo enemy) {
		tegenstander = enemy;
	} 

	public static CharInfo GetEnemyInfo() {
		return tegenstander;
	}

	public static void SetMapPosition(Vector3 pos) {
		mapPosition = pos;
	}

	public static Vector3 GetMapPosition() {
		return mapPosition;
	}

	public static void SetBarPosition(Vector3 pos) {
		barPosition = pos;
	}

	public static Vector3 GetBarPosition() {
		return barPosition;
	}
}
