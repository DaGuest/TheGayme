using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIMover {
	public delegate void OnSliderComplete();
	public event OnSliderComplete onSliderComplete;
	[SerializeField] Image barImage = null;
	[SerializeField] Text naamTekst = null;
	[SerializeField] Text levelTekst = null;

	Slider healthSlider;

	void Awake() {
		healthSlider = gameObject.GetComponentInChildren<Slider>();
	}

	public void setHealthSlider(float value) {
		StartCoroutine(SlideHealth(value));
	}

	IEnumerator SlideHealth(float value) {
		while(healthSlider.value > value && healthSlider.value > 0) {
			healthSlider.value -= 0.01f; 
			ChangeSliderColor();
			yield return new WaitForSeconds(0.01f);
		}
		if (onSliderComplete != null) {
			onSliderComplete();
		}
	}

	void ChangeSliderColor() {
		if (healthSlider.value <= 0.7 && healthSlider.value >= 0.3) {
			barImage.color = Color.yellow;
		}
		else if (healthSlider.value < 0.3 && healthSlider.value >= 0.01) {
			barImage.color = Color.red;
		}
		else if (healthSlider.value < 0.01) {
			barImage.color = Color.clear;
		}
	}

	public void SetNaam(string naam) {
		naamTekst.text = naam;
	}

	public void SetLevel(int level) {
		levelTekst.text = "" + level;
	}
 }
