using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO Call this via LevelManager.getInstance().getUIElements()
public class HealthBarUI : MonoBehaviour {
	[SerializeField] Image bar;

	public void updateHealthBar(float currentHealth, float maxHealth) {
		bar.fillAmount = currentHealth / maxHealth;
	}
}
