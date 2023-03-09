using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	InventoryUI inventoryUI;
	HealthBarUI healthBarUI;

	void Awake() {
		inventoryUI = GetComponentInChildren<InventoryUI>();
		healthBarUI = GetComponentInChildren<HealthBarUI>();
	}

	// Getters
	public InventoryUI getInventoryUI() { return inventoryUI; }
	public HealthBarUI getHealthBarUI() { return healthBarUI; }
}
