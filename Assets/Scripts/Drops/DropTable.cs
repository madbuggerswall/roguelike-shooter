using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DropTable {
	DropEntry[] dropEntries;
	DropEntry[] dropEntriesCDF;

	public DropTable(params DropEntry[] dropEntries) {
		this.dropEntries = dropEntries;
		this.dropEntriesCDF = getDropEntriesAsCDF();
	}

	public Collectible getRandomDrop() {
		float randomValue = Random.value;
		foreach (DropEntry dropEntry in dropEntriesCDF)
			if (randomValue < dropEntry.getProbability())
				return dropEntry.getPrefab();

		return null;
	}

	DropEntry[] getDropEntriesAsCDF() {
		DropEntry[] dropEntriesCDF = dropEntries;
		dropEntriesCDF[0].setProbability(dropEntries[0].getProbability());

		for (int i = 1; i < dropEntries.Length; i++)
			dropEntriesCDF[i].setProbability(dropEntries[i].getProbability() + dropEntriesCDF[i - 1].getProbability());

		checkDropProbabilities(dropEntriesCDF);

		return dropEntriesCDF;
	}

	void checkDropProbabilities(DropEntry[] dropEntriesCDF) {
		foreach (DropEntry dropEntry in dropEntriesCDF)
			if (System.Math.Round(dropEntry.getProbability(), 3) > 1f)
				throw new System.Exception("Drop probability cannot be greater that 1f");
	}

}

