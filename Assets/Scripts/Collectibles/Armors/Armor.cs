using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : Collectible, IPoolable {
	[SerializeField] int maxShieldAmount;
	[SerializeField] int shieldAmount;

	void OnEnable() {
		reset();
	}

	public override void onCollect() {
		LevelManager levelManager = LevelManager.getInstance();
		levelManager.getParticles().spawnParticles(this);
		levelManager.getSoundManager().getCollectibleSound().play(this);
		levelManager.getUIManager().getInventoryUI().updateShieldAmount(shieldAmount, maxShieldAmount);
	}

	public void takeDamage(IDamager damager) {
		shieldAmount = Mathf.Max(shieldAmount - damager.getDamage(), 0);
		LevelManager.getInstance().getUIManager().getInventoryUI().updateShieldAmount(shieldAmount, maxShieldAmount);

		if (shieldAmount <= 0)
			shatter();
	}

	void shatter() {
		LevelManager.getInstance().getParticles().spawnParticles(this);
		LevelManager.getInstance().getHero().getInventory().discardArmor();
		returnToPool();
	}

	// IPoolable
	public override void reset() {
		shieldAmount = maxShieldAmount;
	}

	public override void returnToPool() {
		gameObject.SetActive(false);
		transform.SetParent(LevelManager.getInstance().getDropManager().getPoolTransform());
	}

	// Getters
	public int getShieldAmount() { return shieldAmount; }
	public int getMaxShieldAmount() { return shieldAmount; }
}
