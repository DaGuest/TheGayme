using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersierController : MonoBehaviour {
	[SerializeField] HealthBar playerHealthBar = null;
	[SerializeField] HealthBar tegenstanderHealthBar = null;
	[SerializeField] Window windowGroot = null;
	[SerializeField] Menu mainMenu = null;
	[SerializeField] Menu flirtMenu = null;
	[SerializeField] Menu itemMenu = null;
	[SerializeField] PlayerObject player = null;
	[SerializeField] PlayerObject tegenstander = null;
	[SerializeField] Animator transitionAnimator = null;

	bool isPlayerTurn = true;  //true = player, false = tegenstander 
	bool tekstComplete = false;
	bool sliderComplete = false;
	int isEffective = 0;
	CharInfo enemy;

	void Awake() {
		player.SetCharInfo(InfoHolder.GetPlayerInfo());
		tegenstander.SetCharInfo(InfoHolder.GetEnemyInfo());
	}

	void Start() {
		flirtMenu.LoadFlirts(player.GetActionNamen());
		tegenstanderHealthBar.SetNaam(tegenstander.GetNaam());
		player.onHealthChange += playerHealthBar.setHealthSlider;
		player.onEffective += Effective;
		tegenstander.onHealthChange += tegenstanderHealthBar.setHealthSlider;
		tegenstander.onEffective += Effective;
		playerHealthBar.onSliderComplete += SliderComplete;
		tegenstanderHealthBar.onSliderComplete += SliderComplete;
		windowGroot.onTekstComplete += TekstComplete;
		mainMenu.onAction += PlayerAction;
		flirtMenu.onAction += PlayerAction;
		itemMenu.onAction += PlayerAction;
		StartCoroutine(StartScene());
	}

	IEnumerator StartScene() {
		yield return new WaitForSeconds(2f);
		windowGroot.QueueMessage(tegenstander.GetNaam() + " want's to flirt!");
		windowGroot.QueueMessage("Go! GAYZA!");
		windowGroot.ShowQueuedTekst();
		yield return new WaitUntil(() => tekstComplete);
		tekstComplete = false;
		windowGroot.ShowNoTekst();
		tegenstanderHealthBar.Show();
		playerHealthBar.Show();
		mainMenu.Show(true);
	}

	void TekstComplete() {
		tekstComplete = true;
	}

	void SliderComplete() {
		sliderComplete = true;
	}

	void Effective(int code) {
		isEffective = code;
	}

	void NextRound() {
		windowGroot.ShowNoTekst();
		isPlayerTurn = !isPlayerTurn;
		if (player.GetHealth() <= 0 || tegenstander.GetHealth() <= 0) {
			PlayerObject playerToDie = (player.GetHealth() <= 0) ? player : tegenstander;
			PlayerDead(playerToDie);
		}
		else if (isPlayerTurn) {
			mainMenu.Show(true);
		}
		else {
			TegenstanderAction();
		}
	}

	void ShowFlirtMenu() {
		mainMenu.Show(false);
		flirtMenu.Show(true);
	}

	void ShowItemMenu() {
		mainMenu.Show(false);
		itemMenu.Show(true);
	}

	void ShowMainMenu() {
		itemMenu.Show(false);
		flirtMenu.Show(false);
		mainMenu.Show(true);
	}

	void PlayerAction(string selection) {
		if (selection.Equals("FLIRT")) {
			ShowFlirtMenu();
		}
		else if (selection.Equals("ITEM")) {
			ShowItemMenu();
		}
		else if (selection.Equals("CANCEL")) {
			ShowMainMenu();
		}
		else if (selection.Equals("RUN")) {
			RunAction();
		}
		else if (selection.Equals("GEZA")) {
			ChangePlayerAction();
		}
		else if (selection.Equals("FAIL")) {

		}
		else {
			StartCoroutine(StartPerformAction(player, tegenstander, player.GetAction(selection)));
		}
	}

	void TegenstanderAction() {
		StartCoroutine(StartPerformAction(tegenstander, player, tegenstander.GetRandomAction()));
	}

	IEnumerator StartPerformAction(PlayerObject friend, PlayerObject foe, Action toPerform) {
		flirtMenu.Show(false);
		itemMenu.Show(false);
		windowGroot.ShowTekst(friend.GetNaam() + " used " + toPerform.GetNaam() + "!");
		yield return new WaitForSeconds(0.5f);
		friend.PerformAction(toPerform.GetNaam());
		yield return new WaitForSeconds(1.5f);
		TakeDamageAnimation();
		foe.ReceiveAction(toPerform);
		yield return new WaitUntil(() => sliderComplete);
		if (isEffective > 0) {
			windowGroot.ShowTekst("It's super effective!");
		}
		else if (isEffective < 0) {
			windowGroot.ShowTekst("It's not very effective...");
		}
		isEffective = 0;
		yield return new WaitForSeconds(1.5f);
		NextRound();
	}

	void TakeDamageAnimation() {
		StartCoroutine(MoveBackAndForth(Camera.main.transform));
		StartCoroutine(MoveBackAndForth(windowGroot.transform));
		StartCoroutine(MoveBackAndForth(playerHealthBar.transform));
		StartCoroutine(MoveBackAndForth(tegenstanderHealthBar.transform));
	}

	IEnumerator MoveBackAndForth(Transform objectTransform) {
		Vector3 originalPosition = objectTransform.localPosition;
		Vector3 direction = (isPlayerTurn) ? Vector3.right : Vector3.up;
		Vector3 goalPosition = originalPosition + (direction);
		for (int i=0; i<2; i++) {
			while (Vector3.Distance(objectTransform.localPosition, goalPosition) > 0.1f) {
				objectTransform.localPosition = Vector3.MoveTowards(objectTransform.localPosition, goalPosition, 0.3f);
				yield return new WaitForFixedUpdate();
			}
			while (Vector3.Distance(objectTransform.localPosition, originalPosition) > 0.1f) {
				objectTransform.localPosition = Vector3.MoveTowards(objectTransform.localPosition, originalPosition, 0.3f);
				yield return new WaitForFixedUpdate();
			}
		}
	}

	void PlayerDead(PlayerObject playerToDie) {
		if (!playerToDie.tag.Equals("Player")) {
			InfoHolder.SetGeilLevel(InfoHolder.GetGeilLevel() - playerToDie.GetFlirtReward());
		}
		playerToDie.DeathAnimation();
		windowGroot.ShowTekst(playerToDie.GetNaam() + " fainted!");
		StartCoroutine(EndGame());
	}

	void RunAction() {
		mainMenu.Show(false);
		windowGroot.ShowTekst("Got away safely!");
		StartCoroutine(EndGame());
	}

	void ChangePlayerAction() {
		StartCoroutine(IChangePlayerAction());
	}

	IEnumerator IChangePlayerAction() {
		mainMenu.Show(false);
		windowGroot.QueueMessage("Can't change GEZA now!");
		windowGroot.ShowQueuedTekst();
		yield return new WaitUntil(() => tekstComplete);
		tekstComplete = false;
		windowGroot.ShowNoTekst();
		mainMenu.Show(true);
	}

	IEnumerator EndGame() {
		yield return new WaitForSeconds(1.0f);
		transitionAnimator.SetTrigger("end");
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("Bar");
	}
}