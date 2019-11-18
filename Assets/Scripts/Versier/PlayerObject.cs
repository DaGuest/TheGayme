using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {
	public delegate void OnHealthChange(float health);
	public event OnHealthChange onHealthChange;
	public delegate void OnEffective(int effectiveAmount);
	public event OnEffective onEffective;

	Animator playerAnimator;
	int health = 10;
	int maxHealth = 10;
	
	CharInfo charInfo;

	void Awake() {
		playerAnimator = gameObject.GetComponent<Animator>();
	}

	void Start() {
		playerAnimator.SetTrigger("moveIntoView");
	}

	public void SetCharInfo(CharInfo charInfo) {
		this.charInfo = charInfo;
		gameObject.GetComponent<SpriteRenderer>().sprite = charInfo.GetSprite();
		health = maxHealth = charInfo.GetHealth();
	}

	public bool LearnAction(KeyValuePair<string, int> action) {
		bool learned = charInfo.AddAction(action);
		if (learned) {
			charInfo.AddHealth(charInfo.GetFlirtReward());
			InfoHolder.SetPlayerInfo(charInfo);
		}
		return learned;
	}

	public string GetNaam() {
		return charInfo.GetNaam();
	}

	public int GetLevel() {
		return charInfo.GetLevel();
	}

	public Action GetAction(string actionNaam) {
		List<Action> actions = charInfo.GetActions();
		foreach (Action action in actions) {
			string tempActionNaam = action.GetNaam();
			if (tempActionNaam.Equals(actionNaam)) {
				return action;
			}
		}
		return new Action("FAIL", 0);
	}

	public Action GetRandomAction() {
		List<Action> actions = charInfo.GetActions();
		return actions[Random.Range(0, actions.Count)];
	}

	public string[] GetActionNamen() {
		List<Action> actions = charInfo.GetActions();
		string[] toReturn = {"-", "-", "-", "-"};
		for (int i = 0; i < 4; i++) {
			toReturn[i] = actions[i].GetNaam();
		}
		return toReturn;
	}

	public float GetHealth() {
		return health;
	}

	public int GetFlirtReward() {
		return charInfo.GetFlirtReward();
	}

	public KeyValuePair<string, int> GetFlirtActionReward() {
		return charInfo.GetActionReward();
	}

	public void ReceiveAction(Action action) {
		int damage = action.GetDamage();
		if (charInfo.GetWeakness().Equals(action.GetNaam())) {
			damage *= 2;
			onEffective(1);
		}
		else if (charInfo.GetStrength().Equals(action.GetNaam())) {
			damage /= 2;
			onEffective(-1);
		}
		health -= damage;
		onHealthChange(health / (float)maxHealth);
	}

	public void PerformAction(string actionNaam) {
		playerAnimator.SetTrigger(actionNaam);
	}

	public void DeathAnimation() {
		playerAnimator.SetTrigger("death");
	}

	public void Die() {
		charInfo.isFlirtable = false;
		InfoHolder.SetEnemyInfo(charInfo);
	}
}
