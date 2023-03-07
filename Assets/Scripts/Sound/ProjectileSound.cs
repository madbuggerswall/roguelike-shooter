using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSound : MonoBehaviour {
	[SerializeField] AudioClip sword;
	[SerializeField] AudioClip axe;
	[SerializeField] AudioClip arrow;
	[SerializeField] AudioClip fire;

	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void playClip(AudioClip audioClip) {
		audioSource.pitch = Random.Range(0.96f, 1.04f);
		audioSource.volume = Random.Range(0.92f, 1.00f);
		audioSource.PlayOneShot(audioClip);
	}

	public void play(Projectile projectile) {
		switch (projectile) {
			case SwordProjectile: playClip(sword); break;
			case AxeProjectile: playClip(axe); break;
			case Arrow: playClip(arrow); break;
			case Fire: playClip(fire); break;
			
			default: playClip(sword); break;
		}
	}
}
