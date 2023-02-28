using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {
	ObjectPool objectPool;
	DropTableContainer dropTableContainer;

	void Awake() {
		objectPool = GetComponentInChildren<ObjectPool>();
		dropTableContainer = new DropTableContainer();
	}

	void Start() {

		// Drop a random weapon. Position of the weapon should be determined by Grid/Map class
		objectPool.spawn(LevelManager.getInstance().getPrefabs().getWeapon<Sword>());
		Events.getInstance().enemyBeaten.AddListener(spawnDrops);
	}

	void spawnDrops(Enemy enemy, Vector2 position) {
		Collectible drop = dropTableContainer.getDropTable(enemy).getRandomDrop();
		if (drop is not null)
			objectPool.spawn(drop, position);
	}
}

