using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {
	[Header("Enemy")]
	[SerializeField] GameObject jellyPrefab;
	[SerializeField] GameObject ghostPrefab;
	[SerializeField] GameObject brutePrefab;
	[SerializeField] GameObject wizardPrefab;

	[Header("Weapon & Projectile")]
	[SerializeField] GameObject swordPrefab;
	[SerializeField] GameObject axePrefab;
	[SerializeField] GameObject bowPrefab;
	[SerializeField] GameObject harpoonPrefab;
	[SerializeField] GameObject arrowPrefab;
	[SerializeField] GameObject firePrefab;


	[Header("Collectibles")]
	[Header("Armors")]
	[SerializeField] GameObject woodShieldPrefab;
	[SerializeField] GameObject bodyArmorPrefab;

	[Header("Containers")]
	[SerializeField] GameObject pot;
	[SerializeField] GameObject chest;

	[Header("Valuables")]
	[SerializeField] GameObject coinPrefab;
	[SerializeField] GameObject ringPrefab;
	[SerializeField] GameObject braceletPrefab;
	[SerializeField] GameObject keyPrefab;

	[Header("Consumables")]
	[SerializeField] GameObject beefPrefab;
	[SerializeField] GameObject potionPrefab;

	[Header("Buffs")]
	[SerializeField] GameObject damageBuffPrefab;
	[SerializeField] GameObject rangeBuffPrefab;
	[SerializeField] GameObject periodBuffPrefab;
	[SerializeField] GameObject magnetBuffPrefab;
	[SerializeField] GameObject healthBuffPrefab;
	[SerializeField] GameObject movementBuffPrefab;


	ObjectPool objectPool;

	void Awake() {
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	void Start() {
		Events.getInstance().enemyBeaten.AddListener(spawnParticles);
	}


	public void spawnParticles(Enemy enemy) {
		objectPool.spawn(getParticles(enemy), enemy.transform.position);
	}

	public void spawnParticles(Projectile projectile) {
		objectPool.spawn(getParticles(projectile), projectile.transform.position);
	}


	public void spawnParticles(Weapon weapon) {
		objectPool.spawn(getParticles(weapon), weapon.transform.position);
	}

	public void spawnParticles(Armor armor) {
		objectPool.spawn(getParticles(armor), armor.transform.position);
	}

	public void spawnParticles(Container container) {
		objectPool.spawn(getParticles(container), container.transform.position);
	}

	public void spawnParticles(Valuable valuable) {
		objectPool.spawn(getParticles(valuable), valuable.transform.position);
	}

	public void spawnParticles(Consumable consumable) {
		objectPool.spawn(getParticles(consumable), consumable.transform.position);
	}

	public void spawnParticles(Buff buff) {
		objectPool.spawn(getParticles(buff), buff.transform.position);
	}


	GameObject getParticles(Enemy enemy) {
		switch (enemy) {
			case Jelly: return jellyPrefab;
			case Ghost: return ghostPrefab;
			case Brute: return brutePrefab;
			case Wizard: return wizardPrefab;

			default: return jellyPrefab;
		}
	}

	GameObject getParticles(Projectile projectile) {
		switch (projectile) {
			case SwordProjectile: return swordPrefab;
			case AxeProjectile: return axePrefab;
			case Arrow: return arrowPrefab;
			case Fire: return firePrefab;

			default: return swordPrefab;
		}
	}


	GameObject getParticles(Weapon weapon) {
		switch (weapon) {
			case Sword: return swordPrefab;
			case Axe: return axePrefab;
			case Bow: return bowPrefab;
			case Scepter: return harpoonPrefab;

			default: return swordPrefab;
		}
	}

	GameObject getParticles(Armor armor) {
		switch (armor) {
			case WoodShield: return woodShieldPrefab;
			case BodyArmor: return bodyArmorPrefab;

			default: return woodShieldPrefab;
		}
	}

	GameObject getParticles(Container container) {
		switch (container) {
			case SmallPot: return pot;
			case Pot: return pot;
			case Chest: return chest;

			default: return pot;
		}
	}

	GameObject getParticles(Valuable valuable) {
		switch (valuable) {
			case Coin: return coinPrefab;
			case Ring: return ringPrefab;
			case Bracelet: return braceletPrefab;
			// case Key: return keyPrefab;

			default: return coinPrefab;
		}
	}

	GameObject getParticles(Consumable consumable) {
		switch (consumable) {
			case Beef: return beefPrefab;
			case Potion: return potionPrefab;

			default: return beefPrefab;
		}
	}

	GameObject getParticles(Buff buff) {
		switch (buff) {
			case DamageBuff: return damageBuffPrefab;
			case RangeBuff: return rangeBuffPrefab;
			case PeriodBuff: return periodBuffPrefab;
			case MagnetBuff: return magnetBuffPrefab;
			case HealthBuff: return healthBuffPrefab;
			case MovementBuff: return movementBuffPrefab;

			default: return damageBuffPrefab;
		}
	}
}
