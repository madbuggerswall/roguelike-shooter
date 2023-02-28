using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DropTableContainer {
	DropTable jellyDrops;
	DropTable ghostDrops;
	DropTable bruteDrops;

	public DropTableContainer() {
		initializeDropTables();
	}

	void initializeDropTables() {
		Prefabs prefabs = LevelManager.getInstance().getPrefabs();

		jellyDrops = new DropTable(
			new DropEntry(.6f, prefabs.getValuable<Coin>()),
			new DropEntry(.1f, prefabs.getValuable<Ring>()),
			new DropEntry(.01f, prefabs.getValuable<Bracelet>()),
			new DropEntry(.01f, prefabs.getConsumable<Beef>()),
			new DropEntry(.01f, prefabs.getConsumable<Potion>()),
			new DropEntry(.01f, prefabs.getArmor<WoodShield>()),
			new DropEntry(.01f, prefabs.getRandomBuff()));

		ghostDrops = new DropTable(
			new DropEntry(.5f, prefabs.getValuable<Coin>()),
			new DropEntry(.2f, prefabs.getValuable<Ring>()),
			new DropEntry(.1f, prefabs.getValuable<Bracelet>()),
			new DropEntry(.05f, prefabs.getConsumable<Beef>()),
			new DropEntry(.05f, prefabs.getConsumable<Potion>()),
			new DropEntry(.01f, prefabs.getArmor<WoodShield>()),
			new DropEntry(.01f, prefabs.getArmor<BodyArmor>()),
			new DropEntry(.01f, prefabs.getRandomBuff()));

		bruteDrops = new DropTable(
			new DropEntry(.4f, prefabs.getValuable<Coin>()),
			new DropEntry(.2f, prefabs.getValuable<Ring>()),
			new DropEntry(.1f, prefabs.getValuable<Bracelet>()),
			new DropEntry(.05f, prefabs.getConsumable<Beef>()),
			new DropEntry(.05f, prefabs.getConsumable<Potion>()),
			new DropEntry(.05f, prefabs.getArmor<WoodShield>()),
			new DropEntry(.05f, prefabs.getArmor<BodyArmor>()),
			new DropEntry(.05f, prefabs.getRandomBuff()));

	}

	public DropTable getDropTable(Enemy enemy) {
		switch (enemy) {
			case Jelly: return jellyDrops;
			case Ghost: return ghostDrops;
			case Brute: return bruteDrops;
			default: return jellyDrops;
		}
	}
}
