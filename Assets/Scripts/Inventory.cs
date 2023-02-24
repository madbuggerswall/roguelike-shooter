using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IEquipable, ICollectible, Collectible, Upgrade
interface IEquipable {
	void onEquip();
}

public class Inventory : MonoBehaviour {
	Weapon weapon;

	int coins;

	HashSet<Collider2D> attractedItems;

	void Awake() {
		attractedItems = new HashSet<Collider2D>();
	}

	void Start() {
		StartCoroutine(checkItemsAround(4f, 0.2f));
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("Collision inventory");

		if (other.gameObject.layer == Layers.weapon) {
			Weapon weapon = other.gameObject.GetComponent<Weapon>();
			this.weapon = weapon;
			equip(weapon);
		}
	}

	void equip(Weapon weapon) {
		this.weapon = weapon;
		this.weapon.transform.SetParent(transform);
		(weapon as IEquipable).onEquip();
	}

	IEnumerator checkItemsAround(float radius, float period) {
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(Layers.weaponMask);

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
		float attractionMul = 2f;
		float equipDistanceSqr = 1f;
		float distanceSqr = Mathf.Infinity;

		while (attractedItems.Contains(item) && distanceSqr > equipDistanceSqr) {
			Vector2 direction = (transform.position - item.transform.position);
			distanceSqr = direction.sqrMagnitude;
			Vector2 movement = attractionMul * direction / distanceSqr;
			item.attachedRigidbody.MovePosition(item.attachedRigidbody.position + movement * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
		}

		if (distanceSqr < equipDistanceSqr)
			equip(item.GetComponent<Weapon>());
			
	}


	public Weapon getWeapon() { return weapon; }
}
