using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : MonoBehaviour {
	[SerializeField] int coinValue;

	public int getCoinValue() { return coinValue; }
}
