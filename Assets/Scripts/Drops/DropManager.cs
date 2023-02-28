using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {
	DropTableContainer dropTableContainer;
	ObjectPool objectPool;

	void Awake() {
		dropTableContainer = new DropTableContainer();
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	void Start() {

		// Drop a random weapon. Position of the weapon should be determined by Grid/Map class
		objectPool.spawn(LevelManager.getInstance().getPrefabs().getWeapon<Sword>());
		Events.getInstance().enemyBeaten.AddListener(spawnDrops);
	}

	void spawnDrops(Enemy enemy, Vector2 position) {
		GameObject drop = dropTableContainer.getDropTable(enemy).getRandomDrop();
		if (drop is not null)
			objectPool.spawn(drop, position);
	}
}

