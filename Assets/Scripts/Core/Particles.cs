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

	public void spawnParticles(Enemy enemy, Vector2 position) {
		objectPool.spawn(getParticles(enemy), position);
	}

	public void spawnParticles(Projectile projectile, Vector2 position) {
		objectPool.spawn(getParticles(projectile), position);
	}

	public void spawnParticles(Vector2 position) {
		objectPool.spawn(coinPrefab, position);
	}

	GameObject getParticles(Enemy enemy) {
		switch (enemy) {
			case Jelly: return jellyPrefab;
			case Ghost: return jellyPrefab;
			case Brute: return jellyPrefab;
			case Wizard: return jellyPrefab;

			default: return jellyPrefab;
		}
	}

	GameObject getParticles(Projectile projectile) {
		switch (projectile) {
			case SwordProjectile: return swordPrefab;
			case AxeProjectile: return swordPrefab;
			case Arrow: return swordPrefab;

			default: return swordPrefab;
		}
	}
}
