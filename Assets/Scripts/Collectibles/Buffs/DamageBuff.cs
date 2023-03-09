using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : Buff {
	float buffAmount = 0.1f;

	public override void apply() {
		Weapon weapon = LevelManager.getInstance().getHero().getInventory().getWeapon();
		

	}
}
