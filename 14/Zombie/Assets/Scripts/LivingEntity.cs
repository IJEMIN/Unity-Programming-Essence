using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임상에서 "살아있고" "공격 당할수 있는"
// 모든 오브젝트들을 위한 기반 설계를 제공
public class LivingEntity : MonoBehaviour, IDamageable {

    public float startingHealth = 100f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }

    void OnEnable () {
        dead = false;
        health = startingHealth;
    }

    public virtual void OnDamage (float damage, Vector3 hitPoint, Vector3 hitNormal) {
        health -= damage;

        if (health <= 0 && !dead) {
            Die (); // 실제 죽는처리 실행
        }
    }

    // 죽었을때 실행될 처리, virutal이기 때문에 확장클래스가 확장가능
    public virtual void Die () {
        dead = true;

        Destroy (gameObject, 10f); // 스스로를 파괴

    }
}