using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour {
	[SerializeField] AudioClip notice;
	[SerializeField] AudioClip damage;
	[SerializeField] AudioClip die;
	[SerializeField] AudioClip melee;
	[SerializeField] AudioClip dash;

	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void playClip(AudioClip audioClip) {
		audioSource.pitch = Random.Range(0.96f, 1.04f);
		audioSource.volume = Random.Range(0.92f, 1.00f);
		audioSource.PlayOneShot(audioClip);
	}

	public void playNotice() { playClip(notice); }
	public void playDamage() { playClip(damage); }
	public void playDie() { playClip(die); }
	public void playMelee() { playClip(melee); }
	public void playDash() { playClip(dash); }
}
