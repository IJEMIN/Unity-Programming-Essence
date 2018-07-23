using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity {
    public Slider healthSlider;

    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip itemPickupClip;
    private AudioSource playerAudioPlayer;

    private Animator playerAnimator;
    public PlayerMovement playerMovement;
    public PlayerShooter playerShooter;

    private void Start () {
        playerAnimator = GetComponent<Animator> ();
        playerAudioPlayer = GetComponent<AudioSource> ();

        healthSlider.value = health;
    }

    public void RestoreHealth (float newHealth) {
        health += newHealth;
        healthSlider.value = health;
    }

    public override void OnDamage (float damage, Vector3 hitPoint, Vector3 hitDirection) {
        base.OnDamage (damage, hitPoint, hitDirection);

        if (!dead) {
            playerAudioPlayer.PlayOneShot (hitClip);
            healthSlider.value = health;

            FlashEffect.instance.Flash ();
        }
    }

    public override void Die () {
        base.Die ();

        healthSlider.value = 0;

        playerAudioPlayer.PlayOneShot (deathClip);
        playerAnimator.SetTrigger ("Die");

        GameManager.instance.Gameover ();

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }

    private void OnTriggerEnter (Collider other) {
        if (!dead) {
            Item item = other.GetComponent<Item> ();

            if (item != null) {
                playerAudioPlayer.PlayOneShot (itemPickupClip);
                item.Use (gameObject);
            }
        }
    }
}