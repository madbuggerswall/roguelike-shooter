using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype/Clone Pattern
public class Prefabs : MonoBehaviour {
	static Prefabs instance;

	[Header("Enemies")]
	[SerializeField] Jelly jelly;
	[SerializeField] Ghost ghost;

	[Header("Projectiles")]
	[SerializeField] Projectile sword;


	void Awake() {
		assertSingleton();
	}

	public Enemy getEnemy(EnemyType enemyType) {
		switch (enemyType) {
			case EnemyType.jelly:
				return jelly;
			case EnemyType.ghost:
				return ghost;
			default:
				return jelly;
		}
	}

	// Singleton
	public static Prefabs getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
