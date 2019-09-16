using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public delegate void OnAction(string selected);
	public event OnAction onAction;

	[SerializeField] Text[] menuOptions;
	[SerializeField] Image arrow;
	[SerializeField] bool leftRightOptions;
	int menuIndex = 0;
	int menuSize;

	void Start() {
		menuSize = menuOptions.Length;
		MoveArrow();
	}

	void Update() {
		if (gameObject.activeSelf) {
			UpDownControls();
			if (leftRightOptions) {
				LeftRightControls();
			}
			if (Input.GetKeyDown(KeyCode.Return)) {
				onAction(menuOptions[menuIndex].text);
			}
			if (Input.GetKeyDown(KeyCode.Escape)) {
				onAction("CANCEL");
			}
		}
	}

	void UpDownControls() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			SetMenuIndex(-1);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			SetMenuIndex(1);
		}
	}

	void LeftRightControls() {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			SetMenuIndex(-2);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			SetMenuIndex(2);
		}
	}

	void SetMenuIndex(int value) {
		menuIndex = Mathf.Abs((menuIndex + value) % menuSize);
		MoveArrow();
	}

	void MoveArrow() {
		Vector3 optionPosition = menuOptions[menuIndex].transform.localPosition;
		arrow.transform.localPosition = optionPosition + new Vector3(-15, 2, 0); 
	}

	public void Show(bool value) {
		gameObject.SetActive(value);
		MoveArrow();
	}

	public void LoadFlirts(string[] flirtNamen) {
		for (int i = 0; i < flirtNamen.Length; i++) {
			menuOptions[i].text = flirtNamen[i];
		}
	}
}