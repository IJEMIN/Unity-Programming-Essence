using UnityEngine;

public class HealthItem : Item {
    public float health = 30;

    public override void Use (GameObject target) {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth> ();

        if (playerHealth != null) {
            playerHealth.RestoreHealth (health);
        }

        base.Use (target);
    }
}