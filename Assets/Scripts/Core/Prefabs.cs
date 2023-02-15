using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype/Clone Pattern
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



	void Awake() {
		assertSingleton();

		jelly = GetComponentInChildren<Jelly>(true);
		ghost = GetComponentInChildren<Ghost>(true);
		brute = GetComponentInChildren<Brute>(true);
		wizard = GetComponentInChildren<Wizard>(true);

		sword = GetComponentInChildren<Sword>(true);
		axe = GetComponentInChildren<Axe>(true);
		bow = GetComponentInChildren<Bow>(true);

		swordProjectile = GetComponentInChildren<SwordProjectile>(true);
		axeProjectile = GetComponentInChildren<AxeProjectile>(true);
		arrow = GetComponentInChildren<Arrow>(true);

	}

	public Enemy getEnemy(EnemyType enemyType) {
		switch (enemyType) {
			case EnemyType.jelly: return jelly;
			case EnemyType.ghost: return ghost;
			case EnemyType.brute: return brute;
			case EnemyType.wizard: return wizard;

			default: return jelly;
		}
	}

	public Projectile getProjectile(ProjectileType projectileType) {
		switch (projectileType) {
			case ProjectileType.sword: return swordProjectile;
			case ProjectileType.axe: return axeProjectile;
			case ProjectileType.arrow: return arrow;

			default: return swordProjectile;
		}
	}

	// Singleton
	public static Prefabs getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
