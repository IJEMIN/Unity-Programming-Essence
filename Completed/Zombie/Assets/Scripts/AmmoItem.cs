using UnityEngine;

public class AmmoItem : Item {
    public int ammo = 30;

    public override void Use (GameObject target) {
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter> ();

        if (playerShooter != null && playerShooter.gun != null) {
            playerShooter.gun.ammoRemain += ammo;
        }

        base.Use (target);
    }
}