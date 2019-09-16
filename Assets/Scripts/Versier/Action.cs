using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action {
	string naam;
	int damage;

	public Action(string naam, int damage) {
		this.naam = naam;
		this.damage = damage;
	}

	public int GetDamage() {
		return damage;
	}

	public string GetNaam() {
		return naam;
	}
}
