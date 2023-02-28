using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICollectible {
	void onCollect();
}

public class Inventory : MonoBehaviour {
	Weapon weapon;
	Armor armor;

	List<Upgrade> upgrades;

	[SerializeField] int coins;

	HashSet<Collider2D> attractedItems;

	void Awake() {
		attractedItems = new HashSet<Collider2D>();
	}

	void Start() {
		StartCoroutine(checkItemsAround(4f, 0.2f));
	}

	void collect(ICollectible collectible) {
		if (collectible is Weapon)
			equip(collectible as Weapon);
		if (collectible is Armor)
			equip(collectible as Armor);
		else if (collectible is Upgrade)
			equip(collectible as Upgrade);
		else if (collectible is Valuable)
			earn(collectible as Valuable);
		else if (collectible is Consumable)
			consume(collectible as Consumable);
	}

	void equip(Weapon weapon) {
		this.weapon = weapon;
		this.weapon.transform.SetParent(transform);
		this.weapon.transform.localPosition = Vector2.zero;

		weapon.onCollect();
	}

	void equip(Armor armor) {
		this.armor = armor;
		this.armor.transform.SetParent(transform);
		this.armor.transform.localPosition = Vector2.zero;

		armor.onCollect();
	}

	void equip(Upgrade upgrade) {
		upgrades.Add(upgrade);
		upgrade.onCollect();
	}

	void earn(Valuable valuable) {
		coins += valuable.getCoinValue();
		valuable.onCollect();
	}

	void consume(Consumable consumable) {
		// Do consumable stuff here
		consumable.onCollect();
	}

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
			collect(item.GetComponent<ICollectible>());
		}
	}

	// Getters
	public Weapon getWeapon() { return weapon; }
	public Armor getArmor() { return armor; }
}
