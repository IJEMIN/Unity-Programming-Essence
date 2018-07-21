using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity {

	public PlayerShooter playerShooter;
	public PlayerMovement playerMovement;

	public Slider healthSlider;
	Animator playerAnimator;
	public AudioClip hitClip;
	public AudioClip deeathClip;
	private AudioSource playerAudioPlayer;

	public Color damageColor = new Color (255, 0, 0, 30);
	public Image damageEffectImage;
	public float flashSpeed = 5f;

	void Start () {
		playerAnimator = GetComponent<Animator> ();
		playerAudioPlayer = GetComponent<AudioSource> ();

		healthSlider.value = health;
	}

	public override void OnDamage (float damage, Vector3 hitPoint, Vector3 hitDirection) {

		base.OnDamage (damage, hitPoint, hitDirection);

		if (!dead) {
			playerAudioPlayer.PlayOneShot (hitClip);
			healthSlider.value = health;

			StopCoroutine ("DamageEffect");
			StartCoroutine ("DamageEffect");
		}
	}

	public void RestoreHealth (float newHealth) {
		health += newHealth;
		healthSlider.value = health;
	}

	private IEnumerator DamageEffect () {
		damageEffectImage.color = damageColor;

		float progress = 0f;

		while (progress < 1f) {
			damageEffectImage.color = Color.Lerp (damageColor, Color.clear, progress);
			progress += Time.time * flashSpeed;
			yield return null;
		}

		damageEffectImage.color = Color.clear;
	}

	public override void Die () {
		healthSlider.value = 0;

		playerMovement.enabled = false;
		playerShooter.enabled = false;

		playerAnimator.SetTrigger ("Die");
		playerAudioPlayer.PlayOneShot (deeathClip);

		GameManager.instance.Gameover ();

		base.Die ();
	}

	void OnTriggerEnter (Collider other) {
		// 아이템과 충돌한 경우 아이템을 먹는다
	}

}