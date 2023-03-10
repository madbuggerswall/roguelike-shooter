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

	[Header("Projectiles")]
	[SerializeField] SwordProjectile swordProjectile;
	[SerializeField] AxeProjectile axeProjectile;
	[SerializeField] Arrow arrow;
	[SerializeField] Fire fire;

	[Header("Collectibles")]
	[Header("Weapons")]
	[SerializeField] Sword sword;
	[SerializeField] Axe axe;
	[SerializeField] Bow bow;
	[SerializeField] Scepter scepter;

	[Header("Armors")]
	[SerializeField] WoodShield woodShield;
	[SerializeField] BodyArmor bodyArmor;

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
	[SerializeField] DamageBuff damageBuff;
	[SerializeField] HealthBuff healthBuff;
	[SerializeField] MagnetBuff magnetBuff;
	[SerializeField] MovementBuff movementBuff;
	[SerializeField] PeriodBuff periodBuff;
	[SerializeField] RangeBuff rangeBuff;

	void Awake() {
		/*
				// Enemies, only EnemySpawner spawns these
				jelly = GetComponentInChildren<Jelly>(true);
				ghost = GetComponentInChildren<Ghost>(true);
				brute = GetComponentInChildren<Brute>(true);
				wizard = GetComponentInChildren<Wizard>(true);

				// Projectiles, only Weapons spawns these
				swordProjectile = GetComponentInChildren<SwordProjectile>(true);
				axeProjectile = GetComponentInChildren<AxeProjectile>(true);
				arrow = GetComponentInChildren<Arrow>(true);

				// Collectibles, only DropManager spawns these
				// Weapons, only DropManager spawns these
				sword = GetComponentInChildren<Sword>(true);
				axe = GetComponentInChildren<Axe>(true);
				bow = GetComponentInChildren<Bow>(true);

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
				damageBuff = GetComponentInChildren<DamageBuff>(true);
				healthBuff = GetComponentInChildren<HealthBuff>(true);
				magnetBuff = GetComponentInChildren<MagnetBuff>(true);
				movementBuff = GetComponentInChildren<MovementBuff>(true);
				periodBuff = GetComponentInChildren<PeriodBuff>(true);
				rangeBuff = GetComponentInChildren<RangeBuff>(true); */
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
			case var _ when typeof(T) == typeof(Scepter): return scepter;

			default: return sword;
		}
	}

	// Projectile
	public Projectile getProjectile<T>() where T : Projectile {
		switch (true) {
			case var _ when typeof(T) == typeof(SwordProjectile): return swordProjectile;
			case var _ when typeof(T) == typeof(AxeProjectile): return axeProjectile;
			case var _ when typeof(T) == typeof(Arrow): return arrow;
			case var _ when typeof(T) == typeof(Fire): return fire;

			default: return swordProjectile;
		}
	}

	// Armor
	public Armor getArmor<T>() where T : Armor {
		switch (true) {
			case var _ when typeof(T) == typeof(WoodShield): return woodShield;
			case var _ when typeof(T) == typeof(BodyArmor): return bodyArmor;

			default: return woodShield;
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
	public Buff getBuff<T>() where T : Buff {
		switch (true) {
			case var _ when typeof(T) == typeof(DamageBuff): return damageBuff;
			case var _ when typeof(T) == typeof(RangeBuff): return rangeBuff;
			case var _ when typeof(T) == typeof(PeriodBuff): return periodBuff;
			case var _ when typeof(T) == typeof(HealthBuff): return healthBuff;
			case var _ when typeof(T) == typeof(MagnetBuff): return magnetBuff;
			case var _ when typeof(T) == typeof(MovementBuff): return movementBuff;

			default: return damageBuff;
		}
	}

	// Random
	public Buff getRandomBuff() {
		switch (Random.Range(0, 6)) {
			case 0: return damageBuff;
			case 1: return rangeBuff;
			case 2: return periodBuff;
			case 3: return healthBuff;
			case 4: return magnetBuff;
			case 5: return movementBuff;
			
			default: return damageBuff;
		}
	}

	public Weapon getRandomWeapon() {
		switch (Random.Range(0, 6)) {
			case 0: return sword;
			case 1: return axe;
			case 2: return bow;
			
			default: return sword;
		}
	}
}
