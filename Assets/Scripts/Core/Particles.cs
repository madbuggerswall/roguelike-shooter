using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {
	[SerializeField] GameObject swordPrefab;
	[SerializeField] GameObject jellyPrefab;

	ObjectPool objectPool;

	void Awake() {
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	public void spawnParticles(EnemyType enemyType, Vector2 position) {
		objectPool.spawn(getEnemyParticles(enemyType), position);
	}

	public void spawnParticles(ProjectileType projectileType, Vector2 position) {
		objectPool.spawn(getProjectileParticles(projectileType), position);
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

	GameObject getProjectileParticles(ProjectileType projectileType) {
		switch (projectileType) {
			case ProjectileType.sword: return swordPrefab;
			case ProjectileType.axe: return swordPrefab;
			case ProjectileType.arrow: return swordPrefab;
			default: return swordPrefab;
		}
	}
}
