using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DropEntry {
	float probability;
	Collectible prefab;

	protected DropEntry(float probability) {
		this.probability = probability;
	}

	public DropEntry(float probability, Collectible prefab) {
		this.probability = probability;
		this.prefab = prefab;
	}

	public void setProbability(float probability) { this.probability = probability; }
	public float getProbability() { return probability; }
	public virtual Collectible getPrefab() { return prefab; }
}

// To defer the random selection
class RandomBuff : DropEntry {
	public RandomBuff(float probability) : base(probability) { }

	public override Collectible getPrefab() {
		return LevelManager.getInstance().getPrefabs().getRandomBuff();
	}
}

// To defer the random selection
class RandomWeapon : DropEntry {
	public RandomWeapon(float probability) : base(probability) { }

	public override Collectible getPrefab() {
		return LevelManager.getInstance().getPrefabs().getRandomWeapon();
	}
}