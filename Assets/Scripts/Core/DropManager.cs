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
		Valuable coinPrefab = Prefabs.getInstance().getValuable(typeof(Coin));
		objectPool.spawn(coinPrefab.gameObject, position);
	}
}
