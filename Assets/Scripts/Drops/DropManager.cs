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
		objectPool.spawn(LevelManager.getInstance().getPrefabs().getBuff<DamageBuff>(), new Vector3(6, 0, 0));
		Events.getInstance().enemyBeaten.AddListener(spawnDrops);
	}

	void spawnDrops(Enemy enemy) {
		Collectible drop = dropTableContainer.getDropTable(enemy).getRandomDrop();
		if (drop is not null)
			objectPool.spawn(drop, enemy.transform.position);
	}

	// For items that need to return to pool via parenting
	public Transform getPoolTransform() { return objectPool.transform; }
}

