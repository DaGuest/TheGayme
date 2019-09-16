using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoHolder {
	static Sprite tegenstanderSprite;
	static string tegenstanderNaam;
	
	public static void SetTegenstanderSprite(Sprite sprite) {
		tegenstanderSprite = sprite;
	}

	public static Sprite GetTegenstanderSprite() {
		return tegenstanderSprite;
	}

	public static void SetTegenstanderNaam(string naam) {
		tegenstanderNaam = naam;
	}

	public static string GetTegenstanderNaam() {
		return tegenstanderNaam;
	}
}
