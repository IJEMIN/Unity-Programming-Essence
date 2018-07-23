using UnityEngine;

// 체력을 회복하는 아이템
public class HealthItem : Item {
    public float health = 30; // 체력을 회복할 수치

    public override void Use (GameObject target) {
        // 전달 받은 게임 오브젝트로부터 PlayerHealth 컴포넌트를 가져오기 시도
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth> ();

        // PlayerHeatlh 컴포넌트가 있다면
        if (playerHealth != null) {
            // 체력 회복 실행
            playerHealth.RestoreHealth (health);
        }

        base.Use (target);
    }
}