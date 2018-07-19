using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

	public AudioClip pickUpClip;
	public virtual void Use (GameObject target) {
		GameManager.instance.PlaySoundEffect (pickUpClip);
		Destroy (gameObject);
	}

	public void Start () {
		Destroy (gameObject, 5f);
	}
}