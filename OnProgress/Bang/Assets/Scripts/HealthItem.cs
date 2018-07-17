using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour, IItem {

	public float health = 30;

	public AudioClip pickSound;

	void Start () {
		Destroy (gameObject, 5f);
	}

	public void Use (GameObject target) {
		PlayerHealth playerHealth = target.GetComponent<PlayerHealth> ();

		if (playerHealth != null) {
			playerHealth.RestoreHealth (health);
		}

		FindObjectOfType<GameManager> ().PlaySoundEffect (pickSound);

		Destroy (gameObject);
	}

}