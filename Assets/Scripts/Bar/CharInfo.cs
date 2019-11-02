using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfo : MonoBehaviour {
    string naam;
    [SerializeField] int level = 5;
    [SerializeField] int health  = 10;
    [SerializeField] Sprite sprite = null;
    List<Action> actions = new List<Action>();
    [SerializeField] string[] actionNamen = null;
    [SerializeField] int[] actionDamage = null;
    [SerializeField] string weakness = null;
    [SerializeField] string strength = null;
    [SerializeField] int flirtReward = 10;
    [SerializeField] string actionRewardNaam = null;
    [SerializeField] int actionRewardDamage = 0;

    void Awake() {
        this.naam = gameObject.name;
        if (this.sprite == null) {
            this.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        MakeActions();
    }

    void MakeActions() {
        actions.Clear();
        for (int i=0; i<actionNamen.Length; i++) {
            Action actionToAdd = new Action(actionNamen[i], actionDamage[i]);
            actions.Add(actionToAdd);
        }
    }

    public string GetNaam() {
        return naam;
    }

    public int GetLevel() {
        return level;
    }

    public int GetHealth() {
        return health;
    }

    public Sprite GetSprite() {
        return sprite;
    }

    public List<Action> GetActions() {
        return actions;
    }

    public string GetWeakness() {
        return weakness;
    }

    public string GetStrength() {
        return strength;
    }

    public int GetFlirtReward() {
        return flirtReward;
    }

    public KeyValuePair<string, int> GetActionReward() {
        return new KeyValuePair<string, int>(actionRewardNaam, actionRewardDamage);
    }

    public bool AddAction(KeyValuePair<string, int> naamDamage) {
        for (int i=0; i<actionNamen.Length; i++) {
            if (actionNamen[i].Equals(naamDamage.Key)) {
                return false;
            }
            if (actionNamen[i].Equals("-")) {
                actionNamen[i] = naamDamage.Key;
                actionDamage[i] = naamDamage.Value;
                MakeActions();
                return true;
            }
        }
        return false;
    }
}
