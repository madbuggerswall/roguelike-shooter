using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	int coinAmount;

	Weapon weapon;
	Armor armor;

	List<Buff> buffs;

	HashSet<Collider2D> attractedItems;

	void Awake() {
		attractedItems = new HashSet<Collider2D>();
		buffs = new List<Buff>();
	}

	void Start() {
		StartCoroutine(checkItemsAround(4f, 0.2f));
	}

	void collect(Collectible collectible) {
		switch (collectible) {
			case Weapon: equip(collectible as Weapon); break;
			case Armor: equip(collectible as Armor); break;
			case Buff: equip(collectible as Buff); break;
			case Valuable: earn(collectible as Valuable); break;
			case Consumable: consume(collectible as Consumable); break;
		}
	}

	void equip(Weapon weapon) {
		Transform weaponSlot = LevelManager.getInstance().getUIManager().getInventoryUI().getWeaponSlot();

		weapon.onCollect();
		this.weapon = weapon;
		this.weapon.transform.SetParent(weaponSlot);
		this.weapon.transform.localPosition = Vector2.zero;
	}

	void equip(Armor armor) {
		Transform armorSlot = LevelManager.getInstance().getUIManager().getInventoryUI().getArmorSlot();

		armor.onCollect();
		this.armor?.returnToPool();
		this.armor = armor;
		this.armor.transform.SetParent(armorSlot);
		this.armor.transform.localPosition = Vector2.zero;
	}

	void equip(Buff buff) {
		buff.onCollect();
		Transform buffSlot = LevelManager.getInstance().getUIManager().getInventoryUI().getEmptyBuffSlot();

		if (buffSlot is null) return;

		buffs.Add(buff);
		buff.transform.SetParent(buffSlot);
		buff.transform.localPosition = Vector2.zero;
	}

	void earn(Valuable valuable) {
		InventoryUI inventoryUI = LevelManager.getInstance().getUIManager().getInventoryUI();
		coinAmount += valuable.getCoinValue();
		inventoryUI.updateCoinAmount(coinAmount);
		valuable.onCollect();
	}

	void consume(Consumable consumable) {
		consumable.onCollect();
	}

	// Magnet
	IEnumerator checkItemsAround(float radius, float period) {
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(Layers.collectibleMask);

		List<Collider2D> itemsAround = new List<Collider2D>();

		while (true) {
			Physics2D.OverlapCircle(transform.position, radius, contactFilter, itemsAround);

			foreach (Collider2D item in itemsAround)
				if (attractedItems.Add(item))
					StartCoroutine(attractItem(item));

			attractedItems.IntersectWith(itemsAround);
			yield return new WaitForSeconds(period);
		}
	}

	IEnumerator attractItem(Collider2D item) {
		float attractionMul = 4f;
		float equipDistance = 1f;
		float equipDistanceSqr = equipDistance * equipDistance;
		float distanceSqr = Mathf.Infinity;

		while (attractedItems.Contains(item) && distanceSqr > equipDistanceSqr) {
			Vector2 direction = (transform.position - item.transform.position);
			distanceSqr = direction.sqrMagnitude;
			Vector2 movement = attractionMul * direction / distanceSqr;
			item.attachedRigidbody.MovePosition(item.attachedRigidbody.position + movement * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
		}

		// Collect
		if (distanceSqr < equipDistanceSqr) {
			collect(item.GetComponent<Collectible>());
		}
	}

	public void discardArmor() { armor = null; }

	// Getters
	public Weapon getWeapon() { return weapon; }
	public Armor getArmor() { return armor; }
}
