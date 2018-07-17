using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour, IItem {

	public int ammo = 30;

	public AudioClip pickSound;

	void Start () {
		Destroy (gameObject, 5f);
	}

	public void Use (GameObject target) {
		PlayerShooter playerShooter = target.GetComponent<PlayerShooter> ();

		if (playerShooter != null && playerShooter.gun != null) {
			playerShooter.gun.ammoRemain += ammo;
		}

		FindObjectOfType<GameManager> ().PlaySoundEffect (pickSound);

		Destroy (gameObject);
	}
}