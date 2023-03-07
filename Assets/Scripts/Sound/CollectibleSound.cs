using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSound : MonoBehaviour {
	[Header("Collectibles")]
	[SerializeField] AudioClip armor;
	[SerializeField] AudioClip buff;
	[SerializeField] AudioClip consumable;
	[SerializeField] AudioClip weapon;

	[Header("Containers")]
	[SerializeField] AudioClip pot;
	[SerializeField] AudioClip chest;

	[Header("Valuables")]
	[SerializeField] AudioClip coin;
	[SerializeField] AudioClip ring;
	[SerializeField] AudioClip bracelet;

	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void playClip(AudioClip audioClip) {
		audioSource.pitch = Random.Range(0.96f, 1.04f);
		audioSource.volume = Random.Range(0.92f, 1.00f);
		audioSource.PlayOneShot(audioClip);
	}

	public void play(Collectible collectible) {
		switch (collectible) {
			case Armor: playClip(armor); break;
			case Buff: playClip(buff); break;
			case Consumable: playClip(consumable); break;
			case Weapon: playClip(weapon); break;

			case SmallPot: playClip(pot); break;
			case Pot: playClip(pot); break;
			case Chest: playClip(chest); break;

			case Coin: playClip(coin); break;
			case Ring: playClip(ring); break;
			case Bracelet: playClip(bracelet); break;

			default: playClip(coin); break;
		}
	}
}
