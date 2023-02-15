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
		
	}

	GameObject getEnemyParticles(EnemyType enemyType) {
		switch (enemyType) {
			case EnemyType.jelly: return jellyPrefab;
			default: return jellyPrefab;
		}
	}
}
