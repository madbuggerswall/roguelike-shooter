using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {
	ObjectPool objectPool;

	void Awake() {
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	void Start() {
		Events.getInstance().enemyBeaten.AddListener(onEnemyBeaten);
	}

	void onEnemyBeaten(EnemyType enemyType, Vector2 position) {
		// Coin coinPrefab = Prefabs.getInstance().getValuable();
		// objectPool.spawn(coinPrefab.gameObject, position);
	}
}
