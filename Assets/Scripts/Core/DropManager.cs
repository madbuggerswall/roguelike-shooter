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
		objectPool.spawn(Prefabs.getInstance().getWeapon<Sword>());
		Events.getInstance().enemyBeaten.AddListener(onEnemyBeaten);
	}

	void onEnemyBeaten(EnemyType enemyType, Vector2 position) {
		Valuable coinPrefab = Prefabs.getInstance().getValuable<Coin>();
		objectPool.spawn(coinPrefab.gameObject, position);
	}

	// ICollectible getJellyDrop() {
	// 	float coinProb = 0.6f;
	// 	float ringProb = 0.04f;
	// 	float beefProb = 0.04f;

	// 	float[] dropRates = new float[] { 0.6f, 0.04f, 0.04f };
	// 	float[] dropRatesCDF = new float[dropRates.Length];

	// 	for (int i = 0; i < dropRates.Length; i++) {
	// 		if (i == 0)
	// 			dropRatesCDF[i] = dropRates[i];
	// 		else
	// 			dropRatesCDF[i] = dropRates[i] + dropRatesCDF[i - 1];
	// 	}

	// 	float randomValue = Random.value;
	// 	foreach (float dropRate in dropRatesCDF) {
	// 		if (randomValue < dropRate)
	// 	}
	// }
}
