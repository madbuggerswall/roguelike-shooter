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
		 new DropEntry(.6f, prefabs.getValuable<Coin>().gameObject),
		 new DropEntry(.04f, prefabs.getValuable<Ring>().gameObject),
		 new DropEntry(.04f, prefabs.getConsumable<Beef>().gameObject));


		ghostDrops = new DropTable(
			new DropEntry(.5f, prefabs.getValuable<Coin>().gameObject),
			new DropEntry(.2f, prefabs.getValuable<Ring>().gameObject),
			new DropEntry(.1f, prefabs.getValuable<Bracelet>().gameObject),
			new DropEntry(.04f, prefabs.getConsumable<Beef>().gameObject),
			new DropEntry(.04f, prefabs.getBuff<DamageBuff>().gameObject),
			new DropEntry(.04f, prefabs.getBuff<RangeBuff>().gameObject),
			new DropEntry(.04f, prefabs.getBuff<PeriodBuff>().gameObject),
			new DropEntry(.04f, prefabs.getBuff<HealthBuff>().gameObject),
			new DropEntry(.02f, prefabs.getConsumable<Potion>().gameObject));
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
