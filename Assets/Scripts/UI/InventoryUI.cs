using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
	[SerializeField] Text coinAmount;

	[SerializeField] Button weaponSlot;
	[SerializeField] Button armorSlot;
	[SerializeField] Button[] buffs;

	public void updateCoinAmount(int coinAmount) { this.coinAmount.text = coinAmount.ToString(); }
	public Transform getWeaponSlot() { return weaponSlot.transform; }
	public Transform getArmorSlot() { return weaponSlot.transform; }
}
