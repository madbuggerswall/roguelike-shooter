using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct DropEntry {
	float probabilty;
	GameObject prefab;

	public DropEntry(float probabilty, GameObject prefab) {
		this.probabilty = probabilty;
		this.prefab = prefab;
	}
}

class DropTable {
	(float probabilty, GameObject prefab)[] dropEntries;

	public DropTable(params (float probabilty, GameObject prefab)[] dropEntries) {
		this.dropEntries = dropEntries;
	}

	(float, GameObject)[] getDropEntriesAsCDF() {
		(float probabilty, GameObject prefab)[] dropEntriesCDF = dropEntries;
		dropEntriesCDF[0].probabilty = dropEntries[0].probabilty;

		for (int i = 1; i < dropEntries.Length; i++)
			dropEntriesCDF[i].probabilty = dropEntries[i].probabilty + dropEntriesCDF[i - 1].probabilty;

		return dropEntriesCDF;
	}
	
	public GameObject getRandomDrop() {
		(float probabilty, GameObject prefab)[] dropEntriesCDF = getDropEntriesAsCDF();

		float randomValue = Random.value;
		foreach ((float probabilty, GameObject prefab) dropEntry in dropEntriesCDF)
			if (randomValue < dropEntry.probabilty)
				return dropEntry.prefab;

		return null;
	}
}

