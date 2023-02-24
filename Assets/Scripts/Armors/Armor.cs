using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : MonoBehaviour {
	[SerializeField] int shieldAmount;

	public int getShieldAmount() { return shieldAmount; }
}
