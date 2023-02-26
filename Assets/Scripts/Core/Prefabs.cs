using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype/Clone Pattern
// Distribute prefabs to related classes.

public class Prefabs : MonoBehaviour {
	static Prefabs instance;

	[Header("Enemies")]
	[SerializeField] Jelly jelly;
	[SerializeField] Brute brute;
	[SerializeField] Ghost ghost;
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
		assertSingleton();

		// Enemies
		jelly = GetComponentInChildren<Jelly>(true);
		ghost = GetComponentInChildren<Ghost>(true);
		brute = GetComponentInChildren<Brute>(true);
		wizard = GetComponentInChildren<Wizard>(true);

		// Weapons
		sword = GetComponentInChildren<Sword>(true);
		axe = GetComponentInChildren<Axe>(true);
		bow = GetComponentInChildren<Bow>(true);

		// Projectiles
		swordProjectile = GetComponentInChildren<SwordProjectile>(true);
		axeProjectile = GetComponentInChildren<AxeProjectile>(true);
		arrow = GetComponentInChildren<Arrow>(true);

		// Armors
		woodShield = GetComponentInChildren<WoodShield>(true);
		bodyArmor = GetComponentInChildren<BodyArmor>(true);

		// Consumables
		beef = GetComponentInChildren<Beef>(true);
		potion = GetComponentInChildren<Potion>(true);

		// Containers
		chest = GetComponentInChildren<Chest>(true);
		pot = GetComponentInChildren<Pot>(true);
		smallPot = GetComponentInChildren<SmallPot>(true);

		// Valuables
		coin = GetComponentInChildren<Coin>(true);
		ring = GetComponentInChildren<Ring>(true);
		bracelet = GetComponentInChildren<Bracelet>(true);

		// Upgrades
		damageUpgrade = GetComponentInChildren<DamageUpgrade>(true);
		healthUpgrade = GetComponentInChildren<HealthUpgrade>(true);
		magnetUpgrade = GetComponentInChildren<MagnetUpgrade>(true);
		movementUpgrade = GetComponentInChildren<MovementUpgrade>(true);
		periodUpgrade = GetComponentInChildren<PeriodUpgrade>(true);
		rangeUpgrade = GetComponentInChildren<RangeUpgrade>(true);
	}

	// Enemy
	public Enemy getEnemy(EnemyType enemyType) {
		switch (enemyType) {
			case EnemyType.jelly: return jelly;
			case EnemyType.ghost: return ghost;
			case EnemyType.brute: return brute;
			case EnemyType.wizard: return wizard;

			default: return jelly;
		}
	}

	// Weapon
	public Weapon getWeapon<T>() where T : Weapon {
		T weaponType = null;
		switch (weaponType) {
			case Sword: return sword;
			case Axe: return axe;
			case Bow: return bow;

			default: return sword;
		}
	}

	// Projectile
	public Projectile getProjectile<T>() where T : Projectile {
		T projectileType = null;
		switch (projectileType) {
			case SwordProjectile: return swordProjectile;
			case AxeProjectile: return axeProjectile;
			case Arrow: return arrow;

			default: return swordProjectile;
		}
	}

	// Valuable
	public Valuable getValuable<T>() where T : Valuable {
		T valuableType = null;
		switch (valuableType) {
			case Coin: return coin;
			case Ring: return ring;
			case Bracelet: return bracelet;

			default: return coin;
		}
	}
	// Consumable
	public Consumable getConsumable<T>() where T : Consumable {
		T consumableType = null;
		switch (consumableType) {
			case Beef: return beef;
			case Potion: return potion;

			default: return beef;
		}
	}

	// Container
	public Container getContainer<T>() where T : Container {
		T containerType = null;
		switch (containerType) {
			case SmallPot: return smallPot;
			case Pot: return pot;
			case Chest: return chest;

			default: return smallPot;
		}
	}

	// Upgrade
	public Upgrade getUpgrade<T>() where T : Upgrade {
		T upgradeType = null;
		switch (upgradeType) {
			case DamageUpgrade: return damageUpgrade;
			case HealthUpgrade: return healthUpgrade;
			case MagnetUpgrade: return magnetUpgrade;
			case MovementUpgrade: return movementUpgrade;
			case PeriodUpgrade: return periodUpgrade;
			case RangeUpgrade: return rangeUpgrade;

			default: return damageUpgrade;
		}
	}




	// Singleton
	public static Prefabs getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
