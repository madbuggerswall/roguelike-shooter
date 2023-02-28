using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {
	//Consumables
	Beef beef;
	Potion potion;

	// Containers
	Chest chest;
	Pot pot;
	SmallPot smallPot;

	// Valueables
	Coin coin;
	Ring ring;
	Bracelet bracelet;

	// Upgrades
	DamageUpgrade damageUpgrade;
	HealthUpgrade healthUpgrade;
	MagnetUpgrade magnetUpgrade;
	MovementUpgrade movementUpgrade;
	PeriodUpgrade periodUpgrade;
	RangeUpgrade rangeUpgrade;

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
		(float probabilty, ICollectible prefab)[] drops = new (float, ICollectible)[]{
			(.6f, prefabs.getValuable<Coin>()),
			(.04f, prefabs.getValuable<Ring>()),
			(.04f, prefabs.getConsumable<Beef>())
		};

		// To CDF
		(float probabilty, ICollectible prefab)[] dropsCDF = drops;
		for (int i = 0; i < drops.Length; i++) {
			if (i == 0)
				dropsCDF[i].probabilty = drops[i].probabilty;
			else
				dropsCDF[i].probabilty = drops[i].probabilty + dropsCDF[i - 1].probabilty;
		}

		float randomValue = Random.value;
		foreach ((float probabilty, ICollectible prefab) dropEntry in dropsCDF) {
			if (randomValue < dropEntry.probabilty) {
				// objectPool.spawn(dropEntry.prefab, position);
				break;
			}
		}
	}
}
