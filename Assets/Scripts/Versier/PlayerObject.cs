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
	List<Action> actions = new List<Action>();
	
	[SerializeField] string naam = "PlayerObject";
	[SerializeField] string[] attackNamen;
	[SerializeField] int[] attackDamage;
	[SerializeField] List<string> weaknessToAttacks;
	[SerializeField] List<string> strengthToAttacks;

	void Awake() {
		playerAnimator = gameObject.GetComponent<Animator>();
		MakeActions();
	}

	void Start() {
		playerAnimator.SetTrigger("moveIntoView");
	}

	void MakeActions() {
		for (int i = 0; i < attackNamen.Length; i++) {
			actions.Add(new Action(attackNamen[i], attackDamage[i]));
		}	
	}

	public string GetNaam() {
		return naam;
	}

	public void SetNaam(string naam) {
		this.naam = naam;
	}

	public Action GetAction(string actionNaam) {
		foreach (Action action in actions) {
			string tempActionNaam = action.GetNaam();
			if (tempActionNaam.Equals(actionNaam)) {
				return action;
			}
		}
		return new Action("FAIL", 0);
	}

	public Action GetRandomAction() {
		return actions[Random.Range(0, actions.Count)];
	}

	public void ReceiveAction(Action action) {
		int damage = action.GetDamage();
		if (weaknessToAttacks.Contains(action.GetNaam())) {
			damage *= 2;
			onEffective(1);
		}
		else if (strengthToAttacks.Contains(action.GetNaam())) {
			damage /= 2;
			onEffective(-1);
		}
		health -= damage;
		onHealthChange(health / (float)maxHealth);
	}

	public string[] GetActionNamen() {
		string[] toReturn = {"-", "-", "-", "-"};
		for (int i = 0; i < 4; i++) {
			toReturn[i] = actions[i].GetNaam();
		}
		return toReturn;
	}

	public float GetHealth() {
		return health;
	}

	public void PerformAction(string actionNaam) {
		playerAnimator.SetTrigger(actionNaam);
	}

	public void DeathAnimation() {
		playerAnimator.SetTrigger("death");
	}
}
