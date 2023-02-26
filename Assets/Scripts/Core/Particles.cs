using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {
	[SerializeField] GameObject swordPrefab;
	[SerializeField] GameObject jellyPrefab;
	[SerializeField] GameObject coinPrefab;

	ObjectPool objectPool;

	void Awake() {
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	public void spawnParticles(EnemyType enemyType, Vector2 position) {
		objectPool.spawn(getEnemyParticles(enemyType), position);
	}

	public void spawnParticles<T>(T projectileType, Vector2 position) where T : Projectile {
		objectPool.spawn(getProjectileParticles<T>(projectileType), position);
	}

	public void spawnParticles(Vector2 position) {
		objectPool.spawn(coinPrefab, position);
	}

	GameObject getEnemyParticles(EnemyType enemyType) {
		switch (enemyType) {
			case EnemyType.jelly: return jellyPrefab;
			case EnemyType.ghost: return jellyPrefab;
			case EnemyType.brute: return jellyPrefab;
			case EnemyType.wizard: return jellyPrefab;
			default: return jellyPrefab;
		}
	}

	GameObject getProjectileParticles<T>(T projectileType) where T : Projectile {
		switch (projectileType) {
			case SwordProjectile: return swordPrefab;
			case AxeProjectile: return swordPrefab;
			case Arrow: return swordPrefab;

			default: return swordPrefab;
		}
	}
}
