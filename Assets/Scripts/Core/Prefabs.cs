using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype/Clone Pattern
// Distribute prefabs to related classes.

public class Prefabs : MonoBehaviour {
	[Header("Enemies")]
	[SerializeField] Jelly jelly;
	[SerializeField] Ghost ghost;
	[SerializeField] Brute brute;
	[SerializeField] Wizard wizard;

	[Header("Weapons")]
	[SerializeField] Sword sword;
	[SerializeField] Axe axe;
	[SerializeField] Bow bow;

	[Header("Projectiles")]
	[SerializeField] SwordProjectile swordProjectile;
	[SerializeField] AxeProjectile axeProjectile;
	[SerializeField] Arrow arrow;

	[Header("Armors")]
	[SerializeField] WoodShield woodShield;
	[SerializeField] BodyArmor bodyArmor;

	[Header("Collectibles")]
	[Header("Consumables")]
	[SerializeField] Beef beef;
	[SerializeField] Potion potion;

	[Header("Containers")]
	[SerializeField] Chest chest;
	[SerializeField] Pot pot;
	[SerializeField] SmallPot smallPot;

	[Header("Valuables")]
	[SerializeField] Coin coin;
	[SerializeField] Ring ring;
	[SerializeField] Bracelet bracelet;

	[Header("Upgrades")]
	[SerializeField] DamageUpgrade damageUpgrade;
	[SerializeField] HealthUpgrade healthUpgrade;
	[SerializeField] MagnetUpgrade magnetUpgrade;
	[SerializeField] MovementUpgrade movementUpgrade;
	[SerializeField] PeriodUpgrade periodUpgrade;
	[SerializeField] RangeUpgrade rangeUpgrade;


	void Awake() {

		// Enemies, only EnemySpawner spawns these
		jelly = GetComponentInChildren<Jelly>(true);
		ghost = GetComponentInChildren<Ghost>(true);
		brute = GetComponentInChildren<Brute>(true);
		wizard = GetComponentInChildren<Wizard>(true);

		// Weapons, only DropManager spawns these
		sword = GetComponentInChildren<Sword>(true);
		axe = GetComponentInChildren<Axe>(true);
		bow = GetComponentInChildren<Bow>(true);

		// Projectiles, only Weapons spawns these
		swordProjectile = GetComponentInChildren<SwordProjectile>(true);
		axeProjectile = GetComponentInChildren<AxeProjectile>(true);
		arrow = GetComponentInChildren<Arrow>(true);

		// Armors, only DropManager spawns these
		woodShield = GetComponentInChildren<WoodShield>(true);
		bodyArmor = GetComponentInChildren<BodyArmor>(true);

		// Consumables, only DropManager spawns these
		beef = GetComponentInChildren<Beef>(true);
		potion = GetComponentInChildren<Potion>(true);

		// Containers, only DropManager spawns these
		chest = GetComponentInChildren<Chest>(true);
		pot = GetComponentInChildren<Pot>(true);
		smallPot = GetComponentInChildren<SmallPot>(true);

		// Valuables, only DropManager spawns these
		coin = GetComponentInChildren<Coin>(true);
		ring = GetComponentInChildren<Ring>(true);
		bracelet = GetComponentInChildren<Bracelet>(true);

		// Upgrades, only DropManager spawns these
		damageUpgrade = GetComponentInChildren<DamageUpgrade>(true);
		healthUpgrade = GetComponentInChildren<HealthUpgrade>(true);
		magnetUpgrade = GetComponentInChildren<MagnetUpgrade>(true);
		movementUpgrade = GetComponentInChildren<MovementUpgrade>(true);
		periodUpgrade = GetComponentInChildren<PeriodUpgrade>(true);
		rangeUpgrade = GetComponentInChildren<RangeUpgrade>(true);
	}

	// Enemy
	public Enemy getEnemy<T>() where T : Enemy {
		switch (true) {
			case var _ when typeof(T) == typeof(Jelly): return jelly;
			case var _ when typeof(T) == typeof(Ghost): return ghost;
			case var _ when typeof(T) == typeof(Brute): return brute;
			case var _ when typeof(T) == typeof(Wizard): return wizard;

			default: return jelly;
		}
	}

	// Weapon
	public Weapon getWeapon<T>() where T : Weapon {
		switch (true) {
			case var _ when typeof(T) == typeof(Sword): return sword;
			case var _ when typeof(T) == typeof(Axe): return axe;
			case var _ when typeof(T) == typeof(Bow): return bow;

			default: return sword;
		}
	}

	// Projectile
	public Projectile getProjectile<T>() where T : Projectile {
		switch (true) {
			case var _ when typeof(T) == typeof(SwordProjectile): return swordProjectile;
			case var _ when typeof(T) == typeof(AxeProjectile): return axeProjectile;
			case var _ when typeof(T) == typeof(Arrow): return arrow;

			default: return swordProjectile;
		}
	}

	// Valuable
	public Valuable getValuable<T>() where T : Valuable {
		switch (true) {
			case var _ when typeof(T) == typeof(Coin): return coin;
			case var _ when typeof(T) == typeof(Ring): return ring;
			case var _ when typeof(T) == typeof(Bracelet): return bracelet;

			default: return coin;
		}
	}

	// Consumable
	public Consumable getConsumable<T>() where T : Consumable {
		switch (true) {
			case var _ when typeof(T) == typeof(Beef): return beef;
			case var _ when typeof(T) == typeof(Potion): return potion;

			default: return beef;
		}
	}

	// Container
	public Container getContainer<T>() where T : Container {
		switch (true) {
			case var _ when typeof(T) == typeof(SmallPot): return smallPot;
			case var _ when typeof(T) == typeof(Pot): return pot;
			case var _ when typeof(T) == typeof(Chest): return chest;

			default: return smallPot;
		}
	}

	// Upgrade
	public Upgrade getUpgrade<T>() where T : Upgrade {
		switch (true) {
			case var _ when typeof(T) == typeof(DamageUpgrade): return damageUpgrade;
			case var _ when typeof(T) == typeof(HealthUpgrade): return healthUpgrade;
			case var _ when typeof(T) == typeof(MagnetUpgrade): return magnetUpgrade;
			case var _ when typeof(T) == typeof(MovementUpgrade): return movementUpgrade;
			case var _ when typeof(T) == typeof(PeriodUpgrade): return periodUpgrade;
			case var _ when typeof(T) == typeof(RangeUpgrade): return rangeUpgrade;

			default: return damageUpgrade;
		}
	}
}
