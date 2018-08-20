using UnityEngine;
using System;

// 생명체로서 동작할 게임 오브젝트들을 위한 뼈대를 제공한다
// 생명체로서 체력을 가지고, 데미지를 받을 수 있고, 죽을 수 있는 기능을 제공한다
public class LivingEntity : MonoBehaviour, IDamageable {
    public float startingHealth = 100f; // 시작 체력

    public float health { get; protected set; } // 현재 체력
    public bool dead { get; protected set; } // 사망

    public event Action onDeath; // 사망시 발동할 이벤트

    private void OnEnable() {
        // 컴포넌트가 활성화될때 사용할 값들을 초기화
        dead = false; // 사망하지 않은 상태로 시작
        health = startingHealth; // 체력을 시작 체력으로 초기화
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
        // 데미지를 입었을때 실행할 처리
        health -= damage; // 체력감소

        if (health <= 0 && !dead)
        {
            // 체력이 0 보다 작고 아직 죽지 않았다면 죽는 처리를 실행
            Die();
        }
    }


    public virtual void RestoreHealth(float newHealth) {
        // 체력을 회복하는 처리
        if (dead)
        {
            // 이미 사망한 경우 체력을 회복할 수 없음
            return;
        }

        health += newHealth; // 체력 추가
    }


    public virtual void Die() {
        // 사망 처리
        if (onDeath != null)
        {
            // OnDeath 이벤트에 등록된 메서드가 있다면 실행
            onDeath();
        }

        dead = true; // 사망한 상태를 참으로 변경
        Destroy(gameObject, 10f); // 10초 뒤에 게임 오브젝트를 파괴
    }
}