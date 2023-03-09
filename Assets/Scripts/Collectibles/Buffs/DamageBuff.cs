using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : Buff {
	[SerializeField] float buffAmount = 0.1f;

	public override void apply() {
		Weapon weapon = LevelManager.getInstance().getHero().getInventory().getWeapon();
		int buffedDamage = Mathf.CeilToInt(weapon.getBaseProjectileDamage() * (1 + buffAmount));
		weapon.setProjectileDamage(buffedDamage);
	}
}
