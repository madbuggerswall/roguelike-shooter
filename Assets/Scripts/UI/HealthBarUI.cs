using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
	[SerializeField] Image bar;

	void Start() {
		Events.getInstance().playerHit.AddListener(delegate {
			Hero hero = LevelManager.getInstance().getHero();
			updateHealthBar(hero.getHealth(), hero.getMaxHealth());
		});
	}

	public void updateHealthBar(float currentHealth, float maxHealth) {
		bar.fillAmount = currentHealth / maxHealth;
	}
}
