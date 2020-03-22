using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoHolder {
	static CharInfo playerCharInfo;
	public static bool playerInfoLoaded = false;
	static CharInfo tegenstanderCharInfo;
	static Vector3 mapPosition;
	static Vector3 barPosition;
	static int poeplevel = 50;
	static int geillevel = 50;
	static string lastScene = "";
	public static bool showUitleg = true;

	public static void SetPlayerInfo(CharInfo playerInfo) {
		playerCharInfo = playerInfo;
		playerInfoLoaded = true;
	} 

	public static CharInfo GetPlayerInfo() {
		return playerCharInfo;
	}
	
	public static void SetEnemyInfo(CharInfo enemy) {
		tegenstanderCharInfo = enemy;
	} 

	public static CharInfo GetEnemyInfo() {
		return tegenstanderCharInfo;
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

	public static void SetGeilLevel(int value) {
		geillevel = value;
	}

	public static int GetGeilLevel() {
		return geillevel;
	}

	public static string GetLastScene() {
		return lastScene;
	}

	public static void SetLastScene(string sceneName) {
		lastScene = sceneName;
	}

	public static void ResetValues() {
		mapPosition = Vector3.zero;
		barPosition = Vector3.zero;
		playerInfoLoaded = false;
		showUitleg = true;
		lastScene = "";
	}
}
