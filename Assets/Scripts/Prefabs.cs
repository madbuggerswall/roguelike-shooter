using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype/Clone Pattern
public class Prefabs : MonoBehaviour {
	static Prefabs instance;

	[Header("Enemies")]
	[SerializeField] Jelly jelly;
	[SerializeField] Ghost ghost;
	[SerializeField] Brute brute;
	[SerializeField] Wizard wizard;

	[Header("Projectiles")]
	[SerializeField] Projectile sword;


	void Awake() {
		assertSingleton();

		jelly = GetComponentInChildren<Jelly>(true);
		ghost = GetComponentInChildren<Ghost>(true);
		brute = GetComponentInChildren<Brute>(true);
		wizard = GetComponentInChildren<Wizard>(true);
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

	// Singleton
	public static Prefabs getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
