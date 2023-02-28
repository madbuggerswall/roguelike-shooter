using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct DropEntry {
	internal float probability;
	internal GameObject prefab;

	public DropEntry(float probability, GameObject prefab) {
		this.probability = probability;
		this.prefab = prefab;
	}
}

class DropTable {
	DropEntry[] dropEntries;

	public DropTable(params DropEntry[] dropEntries) {
		this.dropEntries = dropEntries;
	}

	DropEntry[] getDropEntriesAsCDF() {
		DropEntry[] dropEntriesCDF = dropEntries;
		dropEntriesCDF[0].probability = dropEntries[0].probability;

		for (int i = 1; i < dropEntries.Length; i++)
			dropEntriesCDF[i].probability = dropEntries[i].probability + dropEntriesCDF[i - 1].probability;

		return dropEntriesCDF;
	}

	public GameObject getRandomDrop() {
		DropEntry[] dropEntriesCDF = getDropEntriesAsCDF();

		float randomValue = Random.value;
		foreach (DropEntry dropEntry in dropEntriesCDF)
			if (randomValue < dropEntry.probability)
				return dropEntry.prefab;

		return null;
	}
}

