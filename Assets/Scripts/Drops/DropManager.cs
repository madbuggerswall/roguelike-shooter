using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {
	ObjectPool objectPool;

	void Awake() {
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	void Start() {

		// Drop a random weapon. Position of the weapon should be determined by Grid/Map class
		objectPool.spawn(LevelManager.getInstance().getPrefabs().getWeapon<Sword>());
		Events.getInstance().enemyBeaten.AddListener(onEnemyBeaten);
	}

	void onEnemyBeaten(Enemy enemy, Vector2 position) {
		switch (enemy) {
			case Jelly:
				spawnJellyDrop(position);
				break;

			default:
				spawnJellyDrop(position);
				break;
		}
	}

	void spawnJellyDrop(Vector2 position) {
		Prefabs prefabs = LevelManager.getInstance().getPrefabs();
		DropTable jellyDrops = new DropTable(
			(.6f, prefabs.getValuable<Coin>().gameObject),
			(.04f, prefabs.getValuable<Ring>().gameObject),
			(.04f, prefabs.getConsumable<Beef>().gameObject)
		);

		GameObject drop = jellyDrops.getRandomDrop();
		if (drop is not null)
			objectPool.spawn(drop, position);
	}
}

